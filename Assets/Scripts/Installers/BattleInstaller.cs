using Core;
using Zenject;

namespace Installers
{
    public class BattleInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<RegistrationMap>().AsSingle();
            Container.BindInterfacesAndSelfTo<PauseController>().AsSingle();
        }
    }
}