using System.Globalization;
using JetBrains.Annotations;

namespace Backend.Fx.ConfigurationSettings.Serializers.BCL;

[UsedImplicitly]
public class LongSerializer : ISettingSerializer<long?>
{
    public string? Serialize(long? setting)
    {
        return setting?.ToString(CultureInfo.InvariantCulture);
    }

    public long? Deserialize(string? value)
    {
        return string.IsNullOrWhiteSpace(value) ? null : long.Parse(value, CultureInfo.InvariantCulture);
    }
}