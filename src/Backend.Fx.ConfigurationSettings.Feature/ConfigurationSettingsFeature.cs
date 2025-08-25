using System.Reflection;
using Backend.Fx.Execution;
using Backend.Fx.Execution.Features;
using JetBrains.Annotations;

namespace Backend.Fx.ConfigurationSettings.Feature;

/// <summary>
/// The feature "Configuration Settings" provides a simple abstraction over an arbitrary key/value configuration
/// setting store. The default <see cref="SettingSerializerFactory"/> already provides serialization to and from
/// string for various configuration setting types, but you can provide your own implementation to extend the
/// functionality.
/// </summary>
/// <typeparam name="TSettingRepository">The abstraction over your key/value store. Instances of this type will
/// be injected with a scoped lifetime.</typeparam>
[PublicAPI]
public class ConfigurationSettingsFeature<TSettingRepository> : IFeature
    where TSettingRepository : class, ISettingRepository
{
    private readonly SettingSerializerFactory _settingSerializerFactory;

    /// <param name="settingSerializerFactory">The factory that provides serializers. A singleton instance is being held.</param>
    public ConfigurationSettingsFeature(SettingSerializerFactory? settingSerializerFactory = null)
    {
        _settingSerializerFactory = settingSerializerFactory ?? new SettingSerializerFactory();
    }

    public void Enable(IBackendFxApplication application)
    {
        application.CompositionRoot.RegisterModules(
            new ConfigurationSettingsModule<TSettingRepository>(_settingSerializerFactory, application.Assemblies));
    }

    public IEnumerable<Assembly> Assemblies => Array.Empty<Assembly>();
}