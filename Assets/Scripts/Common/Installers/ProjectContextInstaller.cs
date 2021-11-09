using SecondBreath.Common.Logger;
using SecondBreath.Game.Battle.Binds;
using SecondBreath.Game.Battle.Characters;
using SecondBreath.Game.Battle.Characters.Configs;
using SecondBreath.Game.States.Binds;
using SecondBreath.Game.Ticks;
using UnityEngine;
using Zenject;

namespace SecondBreath.Common.Installers
{
    public class ProjectContextInstaller : MonoInstaller
    {
        [SerializeField] private BattleCharactersConfig _battleCharactersConfig;
        
        public override void InstallBindings()
        {
            Container
                .Bind<IDebugLogger>()
                .To<UnityDebugLogger>()
                .AsSingle();
            
            Container
                .Bind<IGameTickHandler>()
                .To<GameTickHandler>()
                .AsSingle();
          
            GameStateBinder.Bind(Container);
            BattleBinder.Bind(Container);
            
            BindConfigs();
        }

        private void BindConfigs()
        {
            Container.BindInstance(_battleCharactersConfig).AsSingle();
            Container.Bind<BattleCharactersFactory>().AsSingle();
        }
    }
}