using System.Collections.Concurrent;
using JetBrains.Annotations;

namespace Backend.Fx.ConfigurationSettings.InMem;

[PublicAPI]
public abstract class InMemorySettingRepository : ISettingRepository
{
    protected abstract ConcurrentDictionary<string, Dictionary<string, string?>> SettingsStore { get; }

    public string? GetSerializedValue(string category, string key)
    {
        if (SettingsStore.TryGetValue(category, out var categorizedValues) && categorizedValues.TryGetValue(key, out var value))
        {
            return value;
        }

        return null;
    }

    public void WriteSerializedValue(string category, string key, string? serializedValue)
    {
        var categorizedValues = SettingsStore.GetOrAdd(category, _ => new Dictionary<string, string?>());
        categorizedValues[key] = serializedValue;
    }

    public bool HasAnySetting(string category)
    {
        return SettingsStore.TryGetValue(category, out var categorizedValues) && categorizedValues.Count > 0;
    }
}