using SecondBreath.Common.Logger;
using SecondBreath.Game.Battle.Characters;
using SecondBreath.Game.Battle.Phases;
using SecondBreath.Game.Players;
using SecondBreath.Game.Ticks;
using SecondBreath.Game.UI;
using Zenject;

namespace SecondBreath.Game.Battle.Managers
{
    public class BattlePlayerManager : IBattleManager
    {
        public IPlayer Player { get; } = new GamePlayer(Team.Green);

        private readonly IGameTickWriter _gameTickHandler;
        private readonly IBattleScene _battleScene;
        private readonly IDebugLogger _debugLogger;
        private readonly BattleCharactersFactory _battleCharactersFactory;
        private readonly ViewFactory _viewFactory;
        private readonly ViewProvider _viewProvider;
        private readonly SignalBus _signalBus;

        public BattlePlayerManager(IGameTickWriter gameTickHandler, IBattleScene battleScene,
            IDebugLogger debugLogger, BattleCharactersFactory battleCharactersFactory, ViewFactory viewFactory,
            ViewProvider viewProvider, SignalBus signalBus)
        {
            _gameTickHandler = gameTickHandler;
            _battleScene = battleScene;
            _debugLogger = debugLogger;
            _battleCharactersFactory = battleCharactersFactory;
            _viewFactory = viewFactory;
            _viewProvider = viewProvider;
            _signalBus = signalBus;
        }
        
        public IBattlePreparationController GetPreparationController()
        {
            return new PlayerPreparationController(Player, _battleScene.Field, _gameTickHandler, _debugLogger,
                _battleCharactersFactory, _viewFactory, _viewProvider, _signalBus);
        }
    }
}