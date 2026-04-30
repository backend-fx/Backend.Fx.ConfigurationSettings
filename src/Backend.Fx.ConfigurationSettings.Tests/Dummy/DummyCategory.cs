using NodaTime;

namespace Backend.Fx.ConfigurationSettings.Tests.Dummy;

public class DummyCategory(ISettingRepository settingRepository, ISettingSerializerFactory settingSerializerFactory)
    : SettingsCategory("Dummy", settingRepository, settingSerializerFactory)
{
    public string? MyStringSetting
    {
        get => ReadSetting<string?>(nameof(MyStringSetting));
        set => WriteSetting(nameof(MyStringSetting), value);
    }

    public int MyIntegerSetting
    {
        get => ReadSetting<int?>(nameof(MyIntegerSetting)) ?? 0;
        set => WriteSetting(nameof(MyIntegerSetting), value);
    }

    public int? MyNullableIntegerSetting
    {
        get => ReadSetting<int?>(nameof(MyNullableIntegerSetting));
        set => WriteSetting(nameof(MyNullableIntegerSetting), value);
    }

    public Instant? MyNullableInstantSetting
    {
        get => ReadSetting<Instant?>(nameof(MyNullableInstantSetting));
        set => WriteSetting(nameof(MyNullableInstantSetting), value);
    }

    public LocalDate MyLocalDateSetting
    {
        get => ReadSetting<LocalDate?>(nameof(MyLocalDateSetting)) ?? new LocalDate(2000, 1, 1);
        set => WriteSetting<LocalDate?>(nameof(MyLocalDateSetting), value);
    }
    
    public LocalDate? MyNullableLocalDateSetting
    {
        get => ReadSetting<LocalDate?>(nameof(MyNullableLocalDateSetting));
        set => WriteSetting(nameof(MyNullableLocalDateSetting), value);
    }
}