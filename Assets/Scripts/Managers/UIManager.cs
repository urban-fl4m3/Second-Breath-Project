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
                onHPChange();
            });

            _healthBarTargetData.GetProperty<float>(Attributes.MaxHealth).AsObservable().Subscribe(_ =>
            {
                onHPChange();
            });
        }

        private void onHPChange()
        {
            SetHP(_healthBarTargetData.GetProperty<float>(Attributes.CurrentHealth).Value, _healthBarTargetData.GetProperty<float>(Attributes.MaxHealth).Value);
        }

        private void SetHP(float currentHP, float maxHP)
        {
            print(currentHP / maxHP);
            _healthBar.material.SetFloat("normalizeHPValue", currentHP / maxHP);
        }
    }
}
