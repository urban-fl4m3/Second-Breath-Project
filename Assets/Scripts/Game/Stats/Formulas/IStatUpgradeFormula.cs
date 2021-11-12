namespace SecondBreath.Game.Stats.Formulas
{
    public interface IStatUpgradeFormula
    {
        float GetValue(StatData statData, int level);
    }
}