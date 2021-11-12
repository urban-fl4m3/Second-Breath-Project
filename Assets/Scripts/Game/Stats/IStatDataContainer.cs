namespace SecondBreath.Game.Stats
{
    public interface IStatDataContainer
    {
        float GetStatValue(Stat stat);
        void AddStat(Stat stat, float value);
        void AddStat(Stat stat, StatData statData);
    }
}