using SecondBreath.Common.States;
using Zenject;

namespace SecondBreath.Game.States.Binds
{
    public static class GameStateBinder
    {
        public static void Bind(DiContainer container)
        {
            container
                .Bind<IStateFactory>()
                .To<StateFactory>()
                .AsSingle();
            
            container
                .Bind<IGameStateMachine>()
                .To<GameStateMachine>()
                .AsSingle();
        }
    }
}