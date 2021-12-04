using UnityEngine;

namespace SecondBreath.Game.Stats.Formulas
{
    public class TierMultiplyStatUpgradeFormula : IStatUpgradeFormula
    {
        //todo: move to config
        private const float _upgradePerTier = 0.1f;
        
        public float GetValue(StatData statData, int level)
        {
            var defaultValue = statData.Value;

            if (!statData.IsUpgradeable)
            {
                return defaultValue;
            }

            var upgradeValue = Mathf.Pow(1 + statData.Tier * _upgradePerTier, level + 1);
            return Mathf.Floor(defaultValue * upgradeValue);
        }
    }
}