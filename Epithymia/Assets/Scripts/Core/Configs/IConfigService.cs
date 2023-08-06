namespace Core.Configs
{
    public interface IConfigService
    {
        T GetConfig<T>(string configName) where T : IConfig;
    }
}
