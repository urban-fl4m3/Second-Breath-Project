using SecondBreath.Game.Battle.Characters;
using SecondBreath.Game.Battle.Phases;
using SecondBreath.Game.Players;

namespace SecondBreath.Game.Battle.Managers
{
    public class BattleEnemyManager : IBattleManager
    {
        public IPlayer Player { get; } = new GamePlayer(Team.Red);

        private readonly IBattleScene _battleScene;
        private readonly BattleCharactersFactory _battleCharactersFactory;
        
        public BattleEnemyManager(IBattleScene battleScene, BattleCharactersFactory battleCharactersFactory)
        {
            _battleScene = battleScene;
            _battleCharactersFactory = battleCharactersFactory;
        }
        
        public IBattlePreparationController GetPreparationController()
        {
            return new EnemyPreparationController(Player, _battleScene.Field, _battleCharactersFactory);
        }
    }
}