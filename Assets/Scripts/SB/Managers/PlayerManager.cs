using Managers;
using SB.Character;
using SB.Core;
using SB.Helpers;
using UnityEngine;

namespace SB.Managers
{
    public class PlayerManager : ExtendedMonoBehaviour
    {
        [SerializeField] private CharacterData _playerConfig;
        [SerializeField] private CameraManager _cameraManager;
        [SerializeField] private UIManager _uiManager;

        private Battle.Character _playerCharacter;

        void Start()
        {
            SpawnCharacter();
            _uiManager.VisualizeCharacterHealth(_playerCharacter.characterData.Properties);
        }

        private void SpawnCharacter()
        {
            var characterData = _playerConfig.GetDataModel();
            _playerCharacter =
                InstantiatePrefab(characterData.GetOrCreateProperty<Battle.Character>(Attributes.CharacterPrefab)
                    .Value);
            _playerCharacter.Init();
            _playerCharacter.SetData(characterData);

            _cameraManager.SetMainCameraTarget(_playerCharacter.transform);
        }
    }
}
