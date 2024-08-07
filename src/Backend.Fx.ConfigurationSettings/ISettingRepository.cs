using JetBrains.Annotations;

namespace Backend.Fx.ConfigurationSettings;

[PublicAPI]
public interface ISettingRepository
{
    /// <summary>
    /// Gets the serialized string value for a specific setting key in a specific category
    /// </summary>
    /// <param name="category">The category of the setting</param>
    /// <param name="key">The key of the setting</param>
    /// <returns>The serialized value of the configuration setting, or <c>null</c> when not configured.</returns>
    string? GetSerializedValue(string category, string key);

    /// <summary>
    /// Writes the serialized string value for a specific setting key in a specific category
    /// </summary>
    /// <param name="category">The category of the setting</param>
    /// <param name="key">The key of the setting</param>
    /// <param name="serializedValue">The serialized value of the configuration setting</param>
    void WriteSerializedValue(string category, string key, string? serializedValue);

    /// <summary>
    /// Checks whether any setting within the given category exists in the persistence layer
    /// </summary>
    /// <param name="category"></param>
    /// <returns></returns>
    bool HasAnySetting(string category);
}