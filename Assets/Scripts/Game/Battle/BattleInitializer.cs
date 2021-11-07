using SecondBreath.Common.Initializer;
using SecondBreath.Game.States;

namespace SecondBreath.Game.Battle
{
    public class BattleInitializer : BaseSceneInitializer
    {
        protected override void OnAwake()
        {
            var stateMachine = Container.Resolve<IGameStateMachine>();
            
            stateMachine.CreateInitialStates();
            stateMachine.SetState(GameState.Battle);
        }
    }
}