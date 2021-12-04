using System.Collections.Generic;
using SecondBreath.Game.Battle.Characters.Configs;
using UnityEngine;
using Zenject;

namespace SecondBreath.Game.UI.Views
{
    public class PlayerBattleUnitSelector : BaseView
    {
        [Inject] private DiContainer _diContainer;
        [Inject] private BattleCharactersConfig _battleCharactersConfig;
        
        [SerializeField] private Transform _cardsTransform;
        [SerializeField] private BattleUnitSelectCard _selectionCard;

        private readonly List<BattleUnitSelectCard> _selectionCards = new List<BattleUnitSelectCard>();
        
        protected override void OnInit()
        {
            foreach (var characterInfo in _battleCharactersConfig.CharactersData)
            {
                var id = characterInfo.Key;
                var data = characterInfo.Value;

                var instance = _diContainer.InstantiatePrefab(_selectionCard, _cardsTransform);
                var selectCard = instance.GetComponent<BattleUnitSelectCard>();
                
                selectCard.DescribeUnitData(id, data);
                
                _selectionCards.Add(selectCard);
            }
        }

        protected override void OnHide()
        {
            foreach (var card in _selectionCards)
            {
                card.Clear();
            }
            
            _selectionCards.Clear();
        }
    }
}