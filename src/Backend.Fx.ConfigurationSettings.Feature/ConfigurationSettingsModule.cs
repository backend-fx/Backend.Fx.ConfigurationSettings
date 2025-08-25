using System.Reflection;
using Backend.Fx.Execution.DependencyInjection;
using Backend.Fx.Util;
using Microsoft.Extensions.DependencyInjection;

namespace Backend.Fx.ConfigurationSettings.Feature;

internal class ConfigurationSettingsModule<TSettingRepository> : IModule 
    where TSettingRepository : class, ISettingRepository
{
    private readonly SettingSerializerFactory _settingSerializerFactory;
    private readonly IEnumerable<Assembly> _assemblies;

    public ConfigurationSettingsModule(SettingSerializerFactory settingSerializerFactory, IEnumerable<Assembly> assemblies)
    {
        _settingSerializerFactory = settingSerializerFactory;
        _assemblies = assemblies;
    }

    public void Register(ICompositionRoot compositionRoot)
    {
        compositionRoot.Register(
            ServiceDescriptor.Singleton<ISettingSerializerFactory>(_settingSerializerFactory));

        compositionRoot.Register(
            ServiceDescriptor.Scoped<ISettingRepository, TSettingRepository>());

        foreach (Type settingsCategoryType in _assemblies.GetImplementingTypes<SettingsCategory>())
        {
            compositionRoot.Register(ServiceDescriptor.Scoped(settingsCategoryType, settingsCategoryType));    
        }
            
    }
}