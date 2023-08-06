using System;
using Core.Dialogues;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UI.Panels
{
    public class InputPanel : MonoBehaviour, IPointerClickHandler, IInput
    {
        public event Action Clicked;
        
        void IPointerClickHandler.OnPointerClick(PointerEventData eventData) => 
            Clicked?.Invoke();
    }
}
