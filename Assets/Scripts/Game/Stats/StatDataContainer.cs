using System.Collections.Generic;
using SecondBreath.Common.Logger;
using SecondBreath.Game.Stats.Formulas;

namespace SecondBreath.Game.Stats
{
    public class StatDataContainer : IStatDataContainer
    {
        private readonly int _level;
        private readonly IDebugLogger _debugLogger;
        private readonly IStatUpgradeFormula _upgradeFormula;
        private readonly Dictionary<Stat, float> _stats = new Dictionary<Stat, float>();
        
        public StatDataContainer(int level, IStatUpgradeFormula upgradeFormula, IReadOnlyDictionary<Stat, StatData> statsData, IDebugLogger debugLogger)
        {
            _level = level;
            _debugLogger = debugLogger;
            _upgradeFormula = upgradeFormula;
            
            foreach (var statData in statsData)
            {
                var value = upgradeFormula.GetValue(statData.Value, level);
                _stats.Add(statData.Key, value);
            }
        }

        public float GetStatValue(Stat stat)
        {
            if (!_stats.ContainsKey(stat))
            {
                 _debugLogger.LogError($"Stat container doesn't have stat {stat}!");
                 return 0;
            }

            return _stats[stat];
        }

        public void AddStat(Stat stat, float value)
        {
            if (!_stats.ContainsKey(stat))
            {
                _stats.Add(stat, 0);
            }

            _stats[stat] = value;
        }

        public void AddStat(Stat stat, StatData statData)
        {
            var value = _upgradeFormula.GetValue(statData, _level);
            AddStat(stat, value);
        }
    }
}