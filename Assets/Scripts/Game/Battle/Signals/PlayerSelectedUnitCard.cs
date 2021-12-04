namespace SecondBreath.Game.Battle.Signals
{
    public readonly struct PlayerSelectedUnitCard
    {
        public readonly int UnitID;

        public PlayerSelectedUnitCard(int id)
        {
            UnitID = id;
        }
    }
}