using SecondBreath.Game.Battle.Health;

namespace SecondBreath.Game.Battle.Damage
{
    public interface IDamageable
    {
        IReadOnlyHealth Health { get; }

        void DealDamage(DamageData damageData);
    }
}