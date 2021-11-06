using SecondBreath.Common.Logger;

namespace SecondBreath.Game.Stats.Formulas
{
    public class TierMultiplyStatUpgradeFormula : IStatUpgradeFormula
    {
        private readonly IDebugLogger _debugLogger;
        
        //todo: move to config
        private const float _upgradePerTier = 0.1f;
        
        public TierMultiplyStatUpgradeFormula(IDebugLogger debugLogger)
        {
            _debugLogger = debugLogger;
        }
        
        public float GetValue(StatData statData)
        {
            var defaultValue = statData.Value;

            if (!statData.IsUpgradeable)
            {
                return defaultValue;
            }

            if (statData.Tier == 0)
            {
                _debugLogger.LogError($"Stat data is upgradeable, but tier is 0 (Value: {defaultValue})");
                return defaultValue;
            }
            
            for (var i = 0; i < statData.Tier; i++)
            {
                var res = defaultValue * _upgradePerTier;
                defaultValue += res;
            }

            return defaultValue;
        }
    }
}