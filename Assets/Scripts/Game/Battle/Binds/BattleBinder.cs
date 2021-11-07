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
        }
    }
}