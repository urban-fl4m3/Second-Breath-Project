using SecondBreath.Game.Battle.Characters;
using SecondBreath.Game.Battle.Phases;
using SecondBreath.Game.Players;

namespace SecondBreath.Game.Battle.Managers
{
    public class BattleEnemyManager : IBattleManager
    {
        public IPlayer Player { get; } = new GamePlayer(Team.Red);

        private readonly IBattleField _battleField;
        private readonly BattleCharactersFactory _battleCharactersFactory;
        
        public BattleEnemyManager(IBattleField battleField, BattleCharactersFactory battleCharactersFactory)
        {
            _battleField = battleField;
            _battleCharactersFactory = battleCharactersFactory;
        }
        
        public IBattlePreparationController GetPreparationController()
        {
            return new EnemyPreparationController(Player, _battleField, _battleCharactersFactory);
        }
    }
}