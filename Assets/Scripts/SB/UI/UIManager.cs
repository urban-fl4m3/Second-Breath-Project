using SB.Components.Data;
using SB.Helpers;
using SB.Managers;
using UniRx;
using UnityEngine;

namespace SB.UI
{
    public class UIManager : InjectManager
    {
        private readonly ViewProvider _viewProvider;
        
        private DataModel _healthBarTargetData;
        private static readonly int NormalizeHpValue = Shader.PropertyToID("normalizeHPValue");

        public UIManager(ViewProvider viewProvider)
        {
            _viewProvider = viewProvider;
        }

        public void VisualizeCharacterHealth(DataModel characterData)
        {
            _healthBarTargetData = characterData;
        
            SetHP(_healthBarTargetData.GetOrCreateProperty<float>(Attributes.CurrentHealth).Value, _healthBarTargetData.GetOrCreateProperty<float>(Attributes.MaxHealth).Value);
        
            _healthBarTargetData.GetOrCreateProperty<float>(Attributes.CurrentHealth).AsObservable().Subscribe(_ =>
            {
                OnHPChange();
            });

            _healthBarTargetData.GetOrCreateProperty<float>(Attributes.MaxHealth).AsObservable().Subscribe(_ =>
            {
                OnHPChange();
            });
        }

        private void OnHPChange()
        {
            SetHP(_healthBarTargetData.GetOrCreateProperty<float>(Attributes.CurrentHealth).Value, _healthBarTargetData.GetOrCreateProperty<float>(Attributes.MaxHealth).Value);
        }

        private void SetHP(float currentHP, float maxHP)
        {
            _viewProvider.HealthBar.material.SetFloat(NormalizeHpValue, currentHP / maxHP);
        }
    }
}
