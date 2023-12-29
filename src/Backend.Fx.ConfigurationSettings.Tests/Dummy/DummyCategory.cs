namespace Backend.Fx.ConfigurationSettings.Tests.Dummy;

public class DummyCategory(ISettingRepository settingRepository, ISettingSerializerFactory settingSerializerFactory)
    : SettingsCategory("Dummy", settingRepository, settingSerializerFactory)
{
    public string? MyStringSetting
    {
        get => ReadSetting<string?>(nameof(MyStringSetting));
        set => WriteSetting<string?>(nameof(MyStringSetting), value);
    }
    
    public int MyIntegerSetting
    {
        get => ReadSetting<int?>(nameof(MyIntegerSetting)) ?? 0;
        set => WriteSetting<int>(nameof(MyIntegerSetting), value);
    }
    
    public int? MyNullableIntegerSetting
    {
        get => ReadSetting<int?>(nameof(MyIntegerSetting));
        set => WriteSetting(nameof(MyIntegerSetting), value);
    }
}

