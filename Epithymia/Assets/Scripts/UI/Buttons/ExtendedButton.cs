using System.Collections.Generic;
using Attributes;
using Cysharp.Threading.Tasks;
using TMPro;
using UI.Buttons.Animations;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UI.Buttons
{
    [AddComponentMenu("UI/ExtendedButton")]
    [RequireComponent(typeof(Image))]
    public class ExtendedButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler,
        IPointerUpHandler, IPointerDownHandler
    {
        public bool IsInteractable { get; set; } = true;

        public bool IsVisible
        {
            get => gameObject.activeSelf;
            set => gameObject.SetActive(value);
        }

        public string Text
        {
            get => _textMesh.text;
            set => _textMesh.text = value;
        }
        
        [SerializeField, AutoInit] private Image _buttonImage;

        [SerializeField, AutoInit(isChildrenIncluded: true)]
        private TextMeshProUGUI _textMesh;

        [SerializeReference, SerializeReferenceButton] private List<ButtonAnimation> _enteredAnimations;
        [SerializeReference, SerializeReferenceButton] private List<ButtonAnimation> _exitedAnimations;
        [SerializeReference, SerializeReferenceButton] private List<ButtonAnimation> _clickedAnimations;
        [SerializeReference, SerializeReferenceButton] private List<ButtonAnimation> _uppedAnimations;
        [SerializeReference, SerializeReferenceButton] private List<ButtonAnimation> _downedAnimations;

        public UnityEvent Entered;
        public UnityEvent Exited;
        public UnityEvent Clicked;
        public UnityEvent Upped;
        public UnityEvent Downed;
        
        void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData) =>
            OnEvent(Entered, _enteredAnimations);

        void IPointerExitHandler.OnPointerExit(PointerEventData eventData) =>
            OnEvent(Exited, _exitedAnimations);

        void IPointerClickHandler.OnPointerClick(PointerEventData eventData) =>
            OnEvent(Clicked, _clickedAnimations);

        void IPointerUpHandler.OnPointerUp(PointerEventData eventData) =>
            OnEvent(Upped, _uppedAnimations);

        void IPointerDownHandler.OnPointerDown(PointerEventData eventData) =>
            OnEvent(Downed, _downedAnimations);

        private void OnEvent(UnityEvent unityEvent, List<ButtonAnimation> animations)
        {
            if (!IsInteractable)
                return;

            PlayAnimationsAsync(animations).Forget();
            unityEvent?.Invoke();
        }

        private async UniTaskVoid PlayAnimationsAsync(List<ButtonAnimation> animations)
        {
            foreach (var animation in animations)
            {
                if (animation.IsAwaiting)
                    await animation.AnimateAsync(this);
                else
                    animation.AnimateAsync(this).Forget();
            }
        }
    }
}
