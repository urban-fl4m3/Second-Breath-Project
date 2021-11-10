using Common.Actors;
using SecondBreath.Game.Battle.Characters;
using SecondBreath.Game.Battle.Registration;
using Zenject;

namespace SecondBreath.Game.Battle.Binds
{
    public static class BattleBinder
    {
        public static void Bind(DiContainer container)
        {
            container
                .Bind<IBattleScene>()
                .To<BattleScene>()
                .AsSingle();
            
            container
                .Bind<BattleCharactersFactory>()
                .AsSingle();
            
            container
                .Bind<ITeamObjectRegisterer<IActor>>()
                .To<TeamObjectRegisterer<IActor>>()
                .AsSingle();
        }
    }
}