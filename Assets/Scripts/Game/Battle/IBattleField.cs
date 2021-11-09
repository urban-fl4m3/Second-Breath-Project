using SecondBreath.Game.Players;
using UnityEngine;

namespace SecondBreath.Game.Battle
{
    public interface IBattleField
    {
        Plane GetPlane();

        Rect GetTeamRect(Team team);
    }
}