using SB.Components.Data;
using SB.Helpers;
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
        
        private DataModel _healthBarTargetData;
        private static readonly int NormalizeHpValue = Shader.PropertyToID("normalizeHPValue");

        private CompositeDisposable _sub;

        private HealthBarView _healthBarView;
        
        public UIManager(ViewProvider viewProvider)
        {
            _viewProvider = viewProvider;
            _sub = new CompositeDisposable();
        }

        public void AddCanvas(Canvas canvas)
        {
            _canvas = canvas;
        }
        
        protected override void OnDispose()
        {
            _sub?.Dispose();
        }

        public void VisualizeCharacterHealth(DataModel characterData)
        {
            _healthBarView = Object.Instantiate(_viewProvider.HealthBar, _canvas.transform);
            
            _healthBarTargetData = characterData;
        
            SetHP(_healthBarTargetData.GetOrCreateProperty<float>(Attributes.CurrentHealth).Value, _healthBarTargetData.GetOrCreateProperty<float>(Attributes.MaxHealth).Value);
            
            _sub.Add(_healthBarTargetData.GetOrCreateProperty<float>(Attributes.CurrentHealth).AsObservable().Subscribe(_ =>
            {
                OnHPChange();
            }));

            _sub.Add(_healthBarTargetData.GetOrCreateProperty<float>(Attributes.MaxHealth).AsObservable().Subscribe(_ =>
            {
                OnHPChange();
            }));
        }

        private void OnHPChange()
        {
            SetHP(_healthBarTargetData.GetOrCreateProperty<float>(Attributes.CurrentHealth).Value, _healthBarTargetData.GetOrCreateProperty<float>(Attributes.MaxHealth).Value);
        }

        private void SetHP(float currentHP, float maxHP)
        {
            _healthBarView.HealthBar.material.SetFloat(NormalizeHpValue, currentHP / maxHP);
        }
    }
}
