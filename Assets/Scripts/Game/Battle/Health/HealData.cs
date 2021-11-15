namespace SecondBreath.Game.Battle.Health
{
    public readonly struct HealData
    {
        public float HealAmount { get; }

        public HealData(float healAmount)
        {
            HealAmount = healAmount;
        }
    }
}