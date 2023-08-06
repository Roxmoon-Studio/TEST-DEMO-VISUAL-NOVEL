using Cysharp.Threading.Tasks;
using TMPro;
using UI.Buttons;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Events;
using UnityEngine.ResourceManagement.AsyncOperations;
using VContainer;
using VContainer.Unity;

namespace UI.Windows
{
    public class PopupWindow : MonoBehaviour
    {
        [SerializeField] private TMP_Text _text;
        [SerializeField] private ExtendedButton _okButton;
        [SerializeField] private ExtendedButton _cancelButton;

        public static async UniTask<PopupWindow> OpenAsync(IObjectResolver resolver, Transform parent, string text, UnityAction okCallback, UnityAction cancelCallback = null)
        {
            var handle = Addressables.LoadAssetAsync<GameObject>(nameof(PopupWindow));
            await handle.Task;
            var go = handle.Result;
            var popupWindow = go.GetComponent<PopupWindow>();
            var window = resolver.Instantiate(popupWindow, parent);
            window._text.text = text;
            window._okButton.Clicked.AddListener(okCallback);
            
            if (cancelCallback != null)
                window._cancelButton.Clicked.AddListener(cancelCallback);
            
            window._cancelButton.Clicked.AddListener(() => window.Close(handle));
            InjectSounds(window, resolver);

            return window;
        }

        private static void InjectSounds(PopupWindow window, IObjectResolver resolver)
        {
            var childs = window.GetComponentsInChildren<PlayClickSound>();

            foreach (var child in childs)
            {
                resolver.Inject(child);
            }
        }

        private void Close(AsyncOperationHandle handle)
        {
            Destroy(gameObject);
            Addressables.Release(handle);
        }
    }
}
