namespace DynamiConfig.Business.Services.Interfaces
{
    public interface IConfigManager
    {
        void DoCacheWork(string applicationName, long refreshTimerIntervalInMs);
    }
}
