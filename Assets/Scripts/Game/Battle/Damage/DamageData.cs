namespace SecondBreath.Game.Battle.Damage
{
    public readonly struct DamageData
    {
        public float DamageAmount { get; }

        public DamageData(float damageAmount)
        {
            DamageAmount = damageAmount;
        }
    }
}