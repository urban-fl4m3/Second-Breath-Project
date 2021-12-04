using SecondBreath.Common.Logger;
using SecondBreath.Game.Battle.Binds;
using SecondBreath.Game.Battle.Characters.Configs;
using SecondBreath.Game.Battle.Signals;
using SecondBreath.Game.States.Binds;
using SecondBreath.Game.Ticks;
using SecondBreath.Game.UI;
using UnityEngine;
using Zenject;

namespace SecondBreath.Common.Installers
{
    public class ProjectContextInstaller : MonoInstaller
    {
        [SerializeField] private ViewProvider _viewProvider;
        [SerializeField] private BattleCharactersConfig _battleCharactersConfig;
        
        public override void InstallBindings()
        {
            SignalBusInstaller.Install(Container);
            
            Container
                .Bind<ViewFactory>()
                .AsSingle();
            
            Container
                .Bind<IDebugLogger>()
                .To<UnityDebugLogger>()
                .AsSingle();
            
            Container
                .BindInterfacesTo<GameTickHandler>()
                .AsSingle();
            
            
            GameStateBinder.Bind(Container);
            BattleBinder.Bind(Container);
            
            BindConfigs();
            BindSignals();
        }

        private void BindConfigs()
        {
            Container.BindInstance(_viewProvider).AsSingle();
            Container.BindInstance(_battleCharactersConfig).AsSingle();
        }

        private void BindSignals()
        {
            Container.DeclareSignal<PlayerSelectedUnitCard>().OptionalSubscriber();
        }
    }
}