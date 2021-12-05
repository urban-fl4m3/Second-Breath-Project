using System;
using Common.Actors;
using SecondBreath.Game.Players;

namespace SecondBreath.Game.Battle.Registration
{
    public class RegistrationTeamObjectArgs : EventArgs
    {
        public IActor Obj { get; }
        public Team Team { get; }
        
        public RegistrationTeamObjectArgs(IActor obj, Team team)
        {
            Obj = obj;
            Team = team;
        }
    }
}