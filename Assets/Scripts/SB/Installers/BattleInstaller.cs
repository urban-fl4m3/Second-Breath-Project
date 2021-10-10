using SB.Core;
using Zenject;

namespace SB.Installers
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