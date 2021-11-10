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
        private readonly IBattleScene _battleScene;
        private readonly IDebugLogger _debugLogger;
        private readonly BattleCharactersFactory _battleCharactersFactory;

        public BattlePlayerManager(IGameTickCollection gameTickHandler, IBattleScene battleScene,
            IDebugLogger debugLogger, BattleCharactersFactory battleCharactersFactory)
        {
            _gameTickHandler = gameTickHandler;
            _battleScene = battleScene;
            _debugLogger = debugLogger;
            _battleCharactersFactory = battleCharactersFactory;
        }
        
        public IBattlePreparationController GetPreparationController()
        {
            return new PlayerPreparationController(Player, _battleScene.Field, _gameTickHandler, _debugLogger, _battleCharactersFactory);
        }
    }
}