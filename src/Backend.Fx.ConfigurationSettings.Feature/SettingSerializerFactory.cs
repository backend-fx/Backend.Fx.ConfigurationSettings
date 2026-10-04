using System.Collections.Concurrent;
using System.Reflection;
using JetBrains.Annotations;

namespace Backend.Fx.ConfigurationSettings.Feature;

[PublicAPI]
public class SettingSerializerFactory : ISettingSerializerFactory
{
    private readonly ConcurrentDictionary<Type, ISettingSerializer> _nonNullableAdapters = new();

    protected Dictionary<Type, ISettingSerializer> Serializers { get; }

    public SettingSerializerFactory(IEnumerable<Assembly>? assemblies = null)
    {
        var builtInAssembly = typeof(ISettingSerializer).Assembly;

        // The built-in serializers (shipped in this library) form the baseline. Serializers found
        // in the consumer's assemblies may override a built-in for the same setting type
        // (consumer-wins), but two consumer serializers for the same type are a configuration error.
        Serializers = new Dictionary<Type, ISettingSerializer>();
        RegisterSerializers(new[] { builtInAssembly }, allowOverride: false);

        var consumerAssemblies = (assemblies ?? Enumerable.Empty<Assembly>())
            .Where(a => a != builtInAssembly)
            .Distinct()
            .ToArray();
        RegisterSerializers(consumerAssemblies, allowOverride: true);
    }

    private void RegisterSerializers(IEnumerable<Assembly> assemblies, bool allowOverride)
    {
        var alreadyRegisteredInThisPass = new HashSet<Type>();

        foreach (var typeInfo in assemblies
                     .SelectMany(a => a.ExportedTypes)
                     .Select(t => t.GetTypeInfo())
                     .Where(t => !t.IsAbstract && t.IsClass &&
                                 typeof(ISettingSerializer).GetTypeInfo().IsAssignableFrom(t)))
        {
            var settingType = GetSettingType(typeInfo);

            if (!alreadyRegisteredInThisPass.Add(settingType))
            {
                throw new InvalidOperationException(
                    $"More than one serializer is registered for setting type '{settingType.FullName}'. " +
                    $"Conflicting serializer: '{typeInfo.FullName}'. Provide a single serializer per setting type.");
            }

            if (!allowOverride && Serializers.ContainsKey(settingType))
            {
                throw new InvalidOperationException(
                    $"More than one serializer is registered for setting type '{settingType.FullName}'. " +
                    $"Conflicting serializer: '{typeInfo.FullName}'. Provide a single serializer per setting type.");
            }

            Serializers[settingType] = (ISettingSerializer)Activator.CreateInstance(typeInfo.AsType());
        }
    }

    private static Type GetSettingType(TypeInfo typeInfo)
    {
        var settingTypes = typeInfo.ImplementedInterfaces
            .Where(i => i.GetTypeInfo().IsGenericType &&
                        i.GetGenericTypeDefinition() == typeof(ISettingSerializer<>))
            .Select(i => i.GenericTypeArguments.Single())
            .ToList();

        if (settingTypes.Count != 1)
        {
            throw new InvalidOperationException(
                $"The serializer '{typeInfo.FullName}' must implement exactly one " +
                $"{typeof(ISettingSerializer<>).Name} interface, but implements {settingTypes.Count}.");
        }

        return settingTypes[0];
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