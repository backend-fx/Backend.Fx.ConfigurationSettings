using System.Globalization;
using JetBrains.Annotations;

namespace Backend.Fx.ConfigurationSettings.Serializers.BCL
{
    [UsedImplicitly]
    public class ShortSerializer : ISettingSerializer<short?>
    {
        public string? Serialize(short? setting)
        {
            return setting?.ToString(CultureInfo.InvariantCulture);
        }

        public short? Deserialize(string? value)
        {
            return string.IsNullOrWhiteSpace(value) ? null : short.Parse(value, CultureInfo.InvariantCulture);
        }
    }
}