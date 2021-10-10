using System;
using SB.Common.Attributes;
using SB.Managers;
using UniRx;
using UnityEngine;
using Object = UnityEngine.Object;

namespace SB.UI
{
    public class UIManager : InjectManager
    {
        private readonly ViewProvider _viewProvider;
        private Canvas _canvas;
        
        private static readonly int NormalizeHpValue = Shader.PropertyToID("normalizeHPValue");

        private HealthBarView _healthBarView;
        private IAttribute _healthAttribute;
        private IDisposable _healthSub;
        
        public UIManager(ViewProvider viewProvider)
        {
            _viewProvider = viewProvider;
        }

        public void AddCanvas(Canvas canvas)
        {
            _canvas = canvas;
        }
        
        protected override void OnDispose()
        {
            _healthSub?.Dispose();
        }

        //Add this logic to health bar view and create model and pass attribute to this model
        //Remove this code later
        public void VisualizeCharacterHealth(IAttribute healthAttribute)
        {
            _healthAttribute = healthAttribute;
            _healthBarView = Object.Instantiate(_viewProvider.HealthBar, _canvas.transform);
            
            OnHealthChange();

            _healthSub = null;
            _healthSub = healthAttribute.AbsoluteValue.Subscribe(_ => OnHealthChange());
        }

        private void OnHealthChange()
        {
            var ratio = _healthAttribute.Value / _healthAttribute.MaxValue;
            _healthBarView.HealthBar.material.SetFloat(NormalizeHpValue, ratio);
        }
    }
}
