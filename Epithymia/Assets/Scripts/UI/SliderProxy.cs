using Attributes;
using UI.Buttons;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    [RequireComponent(typeof(Slider))]
    public class SliderProxy : MonoBehaviour
    {
        [SerializeField, AutoInit] private Slider _slider;
        [SerializeField] private ExtendedButton _minusButton;
        [SerializeField] private ExtendedButton _plusButton;
        [SerializeField] private float _stepValue;

        private void Awake()
        {
            _minusButton.Clicked.AddListener(OnMinusButtonClicked);
            _plusButton.Clicked.AddListener(OnPlusButtonClicked);
        }

        private void OnMinusButtonClicked() => 
            _slider.value -= _stepValue;

        private void OnPlusButtonClicked() => 
            _slider.value += _stepValue;
    }
}
