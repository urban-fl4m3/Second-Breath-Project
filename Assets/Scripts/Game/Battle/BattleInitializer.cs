using SecondBreath.Common.Initializer;
using SecondBreath.Game.States;
using SecondBreath.Game.UI;
using UnityEngine;

namespace SecondBreath.Game.Battle
{
    public class BattleInitializer : BaseSceneInitializer
    {
        [SerializeField] private BattleField _battleField;
        [SerializeField] private CanvasController _canvas;
        
        protected override void OnAwake()
        {
            var stateMachine = Container.Resolve<IGameStateMachine>();
            var battleScene = Container.Resolve<IBattleScene>();
            var viewFactory = Container.Resolve<ViewFactory>();

            viewFactory.CanvasController = _canvas;            
            battleScene.Field = _battleField;
            
            stateMachine.CreateInitialStates();
            stateMachine.SetState(GameState.Battle);

        }
    }
}