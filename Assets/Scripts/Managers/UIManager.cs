using Components.Data;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Managers
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private Image _healthBar;
        private DataModel _healthBarTargetData;
    
    
    
        public void visualizeCharacterHealth(DataModel characterData)
        {
            _healthBarTargetData = characterData;
        
            SetHP(_healthBarTargetData.GetProperty<float>(Attributes.CurrentHealth).Value, _healthBarTargetData.GetProperty<float>(Attributes.MaxHealth).Value);
        
            _healthBarTargetData.GetProperty<float>(Attributes.CurrentHealth).AsObservable().Subscribe(_ =>
            {
                OnHPChange();
            });

            _healthBarTargetData.GetProperty<float>(Attributes.MaxHealth).AsObservable().Subscribe(_ =>
            {
                OnHPChange();
            });
        }

        private void OnHPChange()
        {
            SetHP(_healthBarTargetData.GetProperty<float>(Attributes.CurrentHealth).Value, _healthBarTargetData.GetProperty<float>(Attributes.MaxHealth).Value);
        }

        private void SetHP(float currentHP, float maxHP)
        {
            _healthBar.material.SetFloat("normalizeHPValue", currentHP / maxHP);
        }
    }
}
