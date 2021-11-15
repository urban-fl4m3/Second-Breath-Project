namespace SecondBreath.Game.Battle.Health
{
    public interface IHealth : IReadOnlyHealth
    {
        void AddHealth(float health);
        void RemoveHealth(float health);
    }
}