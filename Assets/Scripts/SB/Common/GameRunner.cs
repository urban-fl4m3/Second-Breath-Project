using Cinemachine;
using SB.Common.Attributes;
using SB.Core;
using SB.Managers;
using SB.UI;
using UnityEngine;
using Zenject;

namespace SB.Common
{
    public class GameRunner : ExtendedMonoBehaviour
    {
        [SerializeField] private CinemachineVirtualCamera _mainCamera;
        [SerializeField] private Canvas _canvas;
        
        [Inject] private PlayerManager _playerManager;
        [Inject] private CameraManager _cameraManager;
        [Inject] private UIManager _uiManager;
        
        private void Start()
        {
            _uiManager.AddCanvas(_canvas);
            _cameraManager.AddMainVirtualCamera(_mainCamera);
            
            var character = _playerManager.SpawnPlayerCharacter();
            _cameraManager.SetMainCameraTarget(character.transform);
            _uiManager.VisualizeCharacterHealth(character.Attributes[AttributeType.Health]);
        }
    }
}