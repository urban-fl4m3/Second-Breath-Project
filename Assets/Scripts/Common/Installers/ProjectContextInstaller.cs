using SecondBreath.Common.Logger;
using SecondBreath.Game.Characters.Configs;
using UnityEngine;
using Zenject;

namespace SecondBreath.Common.Installers
{
    public class ProjectContextInstaller : MonoInstaller
    {
        [SerializeField] private BattleCharactersConfig _battleCharactersConfig;
        
        public override void InstallBindings()
        {
            Container.Bind<IDebugLogger>().To<UnityDebugLogger>().AsSingle();
            
            BindConfigs();
        }

        private void BindConfigs()
        {
            Container.BindInstance(_battleCharactersConfig).AsSingle();
        }
    }
}