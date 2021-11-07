using SecondBreath.Common.Logger;

namespace SecondBreath.Game.Battle
{
    public class BattleScene : IBattleScene
    {
        public IBattleField Field
        {
            get => _field;
            set
            {
                if (_field != null)
                {
                    _logger.LogError($"Field was already set. Operation aborted for {value}.");
                    return;
                }
                
                _field = value;
            }
        }
        
        private readonly IDebugLogger _logger;

        private IBattleField _field;

        public BattleScene(IDebugLogger logger)
        {
            _logger = logger;
        }
    }
}