using JetBrains.Annotations;

namespace Backend.Fx.ConfigurationSettings.Serializers.BCL;

[UsedImplicitly]
public class BooleanSerializer : ISettingSerializer<bool?>
{
    public string? Serialize(bool? setting)
    {
        return setting?.ToString();
    }

    public bool? Deserialize(string? value)
    {
        return string.IsNullOrWhiteSpace(value) ? null : bool.Parse(value);
    }
}