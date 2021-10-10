using SB.Battle;

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
            var instance = _container.InstantiatePrefab(_playerConfig.Character);

            _playerCharacter = instance.GetComponent<Character>();
            _playerCharacter.Init(_playerConfig.Attributes);

            return _playerCharacter;
        }
    }
}
