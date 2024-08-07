using System.Globalization;
using JetBrains.Annotations;

namespace Backend.Fx.ConfigurationSettings.Serializers.BCL;

[UsedImplicitly]
public class DateTimeSerializer : ISettingSerializer<DateTime?>
{
    public string? Serialize(DateTime? setting)
    {
        return setting?.ToString("O", CultureInfo.InvariantCulture);
    }

    public DateTime? Deserialize(string? value)
    {
        return string.IsNullOrWhiteSpace(value) ? null : DateTime.Parse(value, CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind);
    }
}