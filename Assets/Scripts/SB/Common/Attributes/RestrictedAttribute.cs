using UniRx;
using UnityEngine;

namespace SB.Common.Attributes
{
    public class RestrictedAttribute : IAttribute
    {
        public float Value
        {
            get => _value;
            set
            {
                _value = value;
                RecalculateAbsoluteValue();
            }
        }

        public float MaxValue
        {
            get => _maxValue;
            set
            {
                _maxValue = value;
                Value = Mathf.Min(_maxValue, _value);
            }
        }

        public float Multiplier
        {
            get => _multiplier;
            set
            {
                _multiplier = value;
                RecalculateAbsoluteValue();
            }
        }

        public IReadOnlyReactiveProperty<float> AbsoluteValue => _absoluteValue;

        private float _value;
        private float _maxValue;
        private float _multiplier;
        
        private readonly ReactiveProperty<float> _absoluteValue;

        public RestrictedAttribute()
        {
            _absoluteValue = new ReactiveProperty<float>();
        }
        
        public RestrictedAttribute(float value, float multiplier = 1)
        {
            _value = value;
            _maxValue = value;
            _multiplier = multiplier;
            
            _absoluteValue = new ReactiveProperty<float>(_value * _multiplier);
        }

        private void RecalculateAbsoluteValue()
        {
            _absoluteValue.Value = _value * _multiplier;
        }
    }
}