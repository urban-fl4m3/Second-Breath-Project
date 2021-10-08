using SB.Battle;
using SB.Managers;
using SB.UI;
using UnityEngine;
using Zenject;

namespace SB.Installers
{
    public class ProjectContextInstaller : MonoInstaller
    {
        [SerializeField] private CharacterData _characterData;
        [SerializeField] private ViewProvider _viewProvider;
        
        public override void InstallBindings()
        {
            Container.BindInstance(_characterData).AsSingle();
            Container.BindInstance(_viewProvider).AsSingle();
            
            Container.BindInterfacesAndSelfTo<PlayerManager>().AsSingle();
            Container.BindInterfacesAndSelfTo<UIManager>().AsSingle();
        }
    }
}