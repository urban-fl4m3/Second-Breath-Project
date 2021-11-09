using System;

namespace SecondBreath.Game.Players
{
    public class GamePlayer : IPlayer
    {
        public Guid Guid { get; }
        public Team Team { get; }

        public GamePlayer(Team team)
        {
            Guid = Guid.NewGuid();
            Team = team;
        }
        
        public bool Equals(IPlayer other)
        {
            if (ReferenceEquals(null, other)) return false;
            return ReferenceEquals(this, other) || Guid.Equals(other.Guid);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == GetType() && Equals((GamePlayer)obj);
        }

        public override int GetHashCode()
        {
            return Guid.GetHashCode();
        }
    }
}