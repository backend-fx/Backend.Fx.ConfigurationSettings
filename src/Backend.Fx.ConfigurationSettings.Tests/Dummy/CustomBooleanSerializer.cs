namespace Backend.Fx.ConfigurationSettings.Tests.Dummy;

/// <summary>
/// A consumer-provided serializer that overrides the built-in boolean serializer.
/// Used to verify the consumer-wins override policy of <see cref="SettingSerializerFactory"/>.
/// </summary>
public class CustomBooleanSerializer : ISettingSerializer<bool?>
{
    public string? Serialize(bool? setting)
    {
        return setting switch
        {
            null => null,
            true => "YES",
            false => "NO"
        };
    }

    public bool? Deserialize(string? value)
    {
        return value switch
        {
            null => null,
            "YES" => true,
            "NO" => false,
            _ => bool.Parse(value)
        };
    }
}
