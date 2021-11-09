using SecondBreath.Common.Logger;
using SecondBreath.Game.Battle.Characters;
using SecondBreath.Game.Battle.Phases;
using SecondBreath.Game.Players;
using SecondBreath.Game.Ticks;

namespace SecondBreath.Game.Battle.Managers
{
    public class BattlePlayerManager : IBattleManager
    {
        public IPlayer Player { get; } = new GamePlayer(Team.Green);

        private readonly IGameTickCollection _gameTickHandler;
        private readonly IBattleField _battleField;
        private readonly IDebugLogger _debugLogger;
        private readonly BattleCharactersFactory _battleCharactersFactory;

        public BattlePlayerManager(IGameTickCollection gameTickHandler, IBattleField battleField,
            IDebugLogger debugLogger, BattleCharactersFactory battleCharactersFactory)
        {
            _gameTickHandler = gameTickHandler;
            _battleField = battleField;
            _debugLogger = debugLogger;
            _battleCharactersFactory = battleCharactersFactory;
        }
        
        public IBattlePreparationController GetPreparationController()
        {
            return new PlayerPreparationController(Player, _battleField, _gameTickHandler, _debugLogger, _battleCharactersFactory);
        }
    }
}