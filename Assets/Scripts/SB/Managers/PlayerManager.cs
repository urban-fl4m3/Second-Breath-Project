using SB.Battle;
using SB.Helpers;
using SB.UI;

namespace SB.Managers
{
    public class PlayerManager : InjectManager
    {
        private Character _playerCharacter;

        private readonly CameraManager _cameraManager;
        private readonly UIManager _uiManager;
        private readonly CharacterData _playerConfig;
        
        public PlayerManager(CameraManager cameraManager, UIManager uiManager, CharacterData playerConfig)
        {
            _cameraManager = cameraManager;
            _playerConfig = playerConfig;
            _uiManager = uiManager;
        }

        protected override void OnActivate()
        {
            SpawnCharacter();
            _uiManager.VisualizeCharacterHealth(_playerCharacter.characterData.Properties);   
        }

        private void SpawnCharacter()
        {
            var characterData = _playerConfig.GetDataModel();
            var instance = _container.InstantiatePrefab(
                characterData.GetOrCreateProperty<Character>(Attributes.CharacterPrefab).Value);

            _playerCharacter = instance.GetComponent<Character>();
            _playerCharacter.Init();
            _playerCharacter.SetData(characterData);

            _cameraManager.SetMainCameraTarget(_playerCharacter.transform);
        }
    }
}
