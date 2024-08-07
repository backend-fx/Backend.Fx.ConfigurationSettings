using System.Globalization;
using JetBrains.Annotations;

namespace Backend.Fx.ConfigurationSettings.Serializers.BCL;

[UsedImplicitly]
public class TimeSpanSerializer : ISettingSerializer<TimeSpan?>
{
    public string? Serialize(TimeSpan? setting)
    {
        return setting?.ToString("g", CultureInfo.InvariantCulture);
    }

    public TimeSpan? Deserialize(string? value)
    {
        return string.IsNullOrWhiteSpace(value) ? null : TimeSpan.Parse(value, CultureInfo.InvariantCulture);
    }
}