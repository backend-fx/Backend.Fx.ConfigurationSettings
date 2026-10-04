using System.Collections.Concurrent;
using System.Reflection;
using JetBrains.Annotations;

namespace Backend.Fx.ConfigurationSettings;

public interface ISettingSerializerFactory
{
    ISettingSerializer<T?> GetSerializer<T>();
}

[PublicAPI]
public class SettingSerializerFactory : ISettingSerializerFactory
{
    private readonly ConcurrentDictionary<Type, ISettingSerializer> _nonNullableAdapters = new();

    protected Dictionary<Type, ISettingSerializer> Serializers { get; }

    public SettingSerializerFactory()
    {
        Serializers = typeof(ISettingSerializer)
            .GetTypeInfo()
            .Assembly
            .ExportedTypes
            .Select(t => t.GetTypeInfo())
            .Where(t => !t.IsAbstract && t.IsClass && typeof(ISettingSerializer).GetTypeInfo().IsAssignableFrom(t))
            .ToDictionary(
                t => t.ImplementedInterfaces
                    .Single(i =>
                        i.GetTypeInfo().IsGenericType && i.GetGenericTypeDefinition() == typeof(ISettingSerializer<>))
                    .GenericTypeArguments.Single(),
                t => (ISettingSerializer)Activator.CreateInstance(t.AsType()));
    }

    public ISettingSerializer<T?> GetSerializer<T>()
    {
        if (Serializers.TryGetValue(typeof(T), out var serializer))
        {
            return (ISettingSerializer<T?>)serializer;
        }

        // Serializers for value types are registered under their nullable type (e.g. int?).
        // When a caller requests a non-nullable value type (e.g. WriteSetting<int>(...)), the
        // return type ISettingSerializer<T?> collapses to ISettingSerializer<int> at runtime
        // (the nullable annotation is erased for an unconstrained T). We therefore wrap the
        // registered int? serializer in an adapter that exposes the non-nullable contract.
        if (typeof(T).IsValueType && Nullable.GetUnderlyingType(typeof(T)) == null)
        {
            var nullableType = typeof(Nullable<>).MakeGenericType(typeof(T));
            if (Serializers.TryGetValue(nullableType, out var nullableSerializer))
            {
                var adapter = _nonNullableAdapters.GetOrAdd(
                    typeof(T),
                    _ =>
                    {
                        var adapterType = typeof(NonNullableValueSerializer<>).MakeGenericType(typeof(T));
                        return (ISettingSerializer)Activator.CreateInstance(adapterType, nullableSerializer)!;
                    });

                return (ISettingSerializer<T?>)adapter;
            }
        }

        throw new ArgumentOutOfRangeException(nameof(T),
            $"No Serializer for Setting Type {typeof(T).Name} available");
    }

    private sealed class NonNullableValueSerializer<TValue> : ISettingSerializer<TValue>
        where TValue : struct
    {
        private readonly ISettingSerializer<TValue?> _inner;

        public NonNullableValueSerializer(ISettingSerializer<TValue?> inner)
        {
            _inner = inner;
        }

        public string? Serialize(TValue setting) => _inner.Serialize(setting);

        public TValue Deserialize(string? value) => _inner.Deserialize(value) ?? default;
    }
}