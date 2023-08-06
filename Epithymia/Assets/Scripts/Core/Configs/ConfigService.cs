using System.Collections.Generic;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.Exceptions;

namespace Core.Configs
{
    public class ConfigService : IConfigService
    {
        private Dictionary<string, IConfig> _cache = new();

        public T GetConfig<T>(string configName) where T : IConfig
        {
            if (_cache.TryGetValue(configName, out var config))
                return (T) config;

            config = LoadConfig<T>(configName);

            return (T) config;
        }

        private T LoadConfig<T>(string configName) where T : IConfig
        {
            var handle = Addressables.LoadAssetAsync<T>(configName);
            var result = handle.WaitForCompletion();
            _cache.Add(configName, result);

            if (handle.IsDone)
                return result;

            throw new OperationException("Asset Operation has not done!");
        }
    }
}
