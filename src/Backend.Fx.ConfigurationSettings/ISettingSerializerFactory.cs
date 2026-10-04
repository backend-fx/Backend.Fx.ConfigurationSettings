namespace Backend.Fx.ConfigurationSettings;

public interface ISettingSerializerFactory
{
    ISettingSerializer<T?> GetSerializer<T>();
}