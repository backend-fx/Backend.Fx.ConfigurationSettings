using JetBrains.Annotations;
using NodaTime.Text;

namespace Backend.Fx.ConfigurationSettings.Serializers.NodaTime;

[UsedImplicitly]
public abstract class NodaTimePatternSerializer<T> : ISettingSerializer<T?> where T : struct
{
    private readonly IPattern<T> _pattern;

    protected NodaTimePatternSerializer(IPattern<T> pattern)
    {
        _pattern = pattern;
    }

    public string? Serialize(T? setting)
    {
        return setting == null ? null : _pattern.Format(setting.Value);
    }

    public T? Deserialize(string? value)
    {
        return string.IsNullOrWhiteSpace(value)
            ? null
            : _pattern.Parse(value!).GetValueOrThrow();
    }

}