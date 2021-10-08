using SB.Battle;
using SB.Helpers;

namespace SB.Managers
{
    public class PlayerManager : InjectManager
    {
        private Character _playerCharacter;
        
        private readonly CharacterData _playerConfig;
        
        public PlayerManager(CharacterData playerConfig)
        {
            _playerConfig = playerConfig;
        }

        public Character SpawnPlayerCharacter()
        {
            var characterData = _playerConfig.GetDataModel();
            var instance = _container.InstantiatePrefab(
                characterData.GetOrCreateProperty<Character>(Attributes.CharacterPrefab).Value);

            _playerCharacter = instance.GetComponent<Character>();
            _playerCharacter.Init(characterData);

            return _playerCharacter;
        }
    }
}
