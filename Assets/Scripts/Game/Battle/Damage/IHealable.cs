using SecondBreath.Game.Battle.Health;

namespace SecondBreath.Game.Battle.Damage
{
    public interface IHealable
    {
        IReadOnlyHealth Health { get; }

        void Heal(HealData healData);
    }
}