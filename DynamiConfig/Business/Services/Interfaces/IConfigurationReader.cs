namespace DynamiConfig.Business.Services.Interfaces
{
    public interface IConfigurationReader
    {
        T GetValue<T>(string key);
    }
}
