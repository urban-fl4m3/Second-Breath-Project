using SecondBreath.Game.Players;

namespace SecondBreath.Game.Battle.Registration
{
    public readonly struct RegistrationTeamObjectArgs<T>
    {
        public T Obj { get; }
        public Team Team { get; }
        
        public RegistrationTeamObjectArgs(T obj, Team team)
        {
            Obj = obj;
            Team = team;
        }
    }
}