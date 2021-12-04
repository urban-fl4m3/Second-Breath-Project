using SecondBreath.Game.Battle.Characters.Configs;
using SecondBreath.Game.Battle.Signals;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace SecondBreath.Game.UI.Views
{
    public class BattleUnitSelectCard : MonoBehaviour
    {
        [Inject] private SignalBus _signalBus;
        
        [SerializeField] private Image _unitIcon;
        [SerializeField] private TextMeshProUGUI _unitName;
        [SerializeField] private Button _button;

        private int _unitId;
        
        public void DescribeUnitData(int id, BattleCharacterData data)
        {
            _unitId = id;
            _unitIcon.sprite = data.Icon;
            _unitName.text = data.Name;
            
            _button.onClick.AddListener(SelectionButtonClicked);
        }

        public void Clear()
        {
            _button.onClick.RemoveListener(SelectionButtonClicked);
        }

        private void SelectionButtonClicked()
        {
            var signal = new PlayerSelectedUnitCard(_unitId);
            _signalBus.Fire(signal);   
        }
    }
}