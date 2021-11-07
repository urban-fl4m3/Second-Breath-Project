using SecondBreath.Common.Initializer;
using SecondBreath.Game.States;
using UnityEngine;

namespace SecondBreath.Game.Battle
{
    public class BattleInitializer : BaseSceneInitializer
    {
        [SerializeField] private BattleField _battleField;
        
        protected override void OnAwake()
        {
            var stateMachine = Container.Resolve<IGameStateMachine>();
            var battleScene = Container.Resolve<IBattleScene>();

            battleScene.Field = _battleField;
            
            stateMachine.CreateInitialStates();
            stateMachine.SetState(GameState.Battle);
        }
    }
}