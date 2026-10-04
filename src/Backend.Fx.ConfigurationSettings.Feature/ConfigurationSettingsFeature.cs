using System.Reflection;
using Backend.Fx.Execution;
using Backend.Fx.Execution.Features;
using JetBrains.Annotations;

namespace Backend.Fx.ConfigurationSettings.Feature;

/// <summary>
/// The feature "Configuration Settings" provides a simple abstraction over an arbitrary key/value configuration
/// setting store. The built-in serializers already provide serialization to and from string for various
/// configuration setting types. To support an additional type (or to override a built-in serializer), implement
/// <see cref="ISettingSerializer{T}"/> anywhere in your application assemblies; it is discovered and registered
/// automatically. A serializer found in an application assembly takes precedence over a built-in serializer for
/// the same setting type.
/// </summary>
/// <typeparam name="TSettingRepository">The abstraction over your key/value store. Instances of this type will
/// be injected with a scoped lifetime.</typeparam>
[PublicAPI]
public class ConfigurationSettingsFeature<TSettingRepository> : IFeature
    where TSettingRepository : class, ISettingRepository
{
    public void Enable(IBackendFxApplication application)
    {
        application.CompositionRoot.RegisterModules(
            new ConfigurationSettingsModule<TSettingRepository>(application.Assemblies));
    }

    public IEnumerable<Assembly> Assemblies => Array.Empty<Assembly>();
}