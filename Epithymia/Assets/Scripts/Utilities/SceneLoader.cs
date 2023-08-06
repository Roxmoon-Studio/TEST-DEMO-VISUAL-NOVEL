using Cysharp.Threading.Tasks;
using UnityEngine.SceneManagement;

namespace Utilities
{
    public static class SceneLoader
    {
        public static async UniTask LoadScene(string scene, LoadSceneMode loadSceneMode = LoadSceneMode.Additive)
        {
            var operation = SceneManager.LoadSceneAsync(scene, loadSceneMode);
            operation.allowSceneActivation = true;
            string activeScene = SceneManager.GetActiveScene().name;
            
            await operation;

            var unloadOperation = SceneManager.UnloadSceneAsync(activeScene);
            await unloadOperation;
        }
    }
}
