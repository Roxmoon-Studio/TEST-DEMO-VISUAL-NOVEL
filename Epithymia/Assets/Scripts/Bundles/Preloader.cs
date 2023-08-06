using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Bundles
{
    public class Preloader : MonoBehaviour
    {
        private const string PRELOAD = "Preload";

        private void Start()
        {
            Addressables.DownloadDependenciesAsync(PRELOAD, true);
        }
    }
}
