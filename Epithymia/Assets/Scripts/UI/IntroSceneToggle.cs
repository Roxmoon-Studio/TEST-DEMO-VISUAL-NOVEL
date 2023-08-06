using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;
using Utilities;

namespace UI
{
    public class IntroSceneToggle : MonoBehaviour
    {
        [SerializeField] private VideoPlayer _videoPlayer;
        [SerializeField, Scene] private string _scene;
        
        private AsyncOperation _loadSceneOperation;
        
        private void OnValidate()
        {
            _videoPlayer = GetComponent<VideoPlayer>();
        }

        private void Start()
        {
            LoadNextScene();
            _videoPlayer.loopPointReached += ToggleNextScene;
        }

        private void Update()
        {
            if (InputHelper.CheckSkipIntro()) 
                ToggleNextScene();
        }

        private void LoadNextScene()
        {
            _loadSceneOperation = SceneManager.LoadSceneAsync(_scene, LoadSceneMode.Additive);
            _loadSceneOperation.allowSceneActivation = false;
            
            string introScene = SceneManager.GetActiveScene().name;
            _loadSceneOperation.completed += _ => SceneManager.UnloadSceneAsync(introScene);
        }

        private void ToggleNextScene(VideoPlayer source = null)
        {
            if (_loadSceneOperation.progress < 0.9f)
                return;

            _loadSceneOperation.allowSceneActivation = true;
        }
    }
}
