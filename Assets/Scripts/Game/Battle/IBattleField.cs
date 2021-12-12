using SecondBreath.Game.Players;
using UnityEngine;

namespace SecondBreath.Game.Battle
{
    public interface IBattleField
    {
        Plane GetPlane();
        Vector3 PathFinding(Vector3 startPosition, Vector3 finishPosition);
        Rect GetTeamRect(Team team);
    }
}