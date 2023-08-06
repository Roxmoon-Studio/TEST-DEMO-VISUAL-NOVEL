using System;
using UnityEngine;
using UnityEngine.Audio;

namespace Audio
{
    public class Volume
    {
        private const float MIN_VALUE = 0.0001f;
        private const float MAX_VALUE = 1f;

        public event Action<float> Changed;

        public float Value
        {
            get => _value;
            set
            {
                _value = Mathf.Clamp(value, MIN_VALUE, MAX_VALUE);
                _mixer.SetFloat(_volumeField, Mathf.Log10(_value) * 20);
                Changed?.Invoke(_value);
            }
        }

        private float _value;
        
        private readonly AudioMixer _mixer;
        private readonly string _volumeField;

        public Volume(AudioMixer mixer, float value, string volumeField)
        {
            _mixer = mixer;
            _volumeField = volumeField;
            Value = value;
        }
    }
}
