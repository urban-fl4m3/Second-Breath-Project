using SecondBreath.Game.Battle.Movement;
using SecondBreath.Game.Players;
using UnityEngine;

namespace SecondBreath.Game.Battle
{
    public interface IBattleField
    {
        Plane GetPlane();
        Vector3 PathFinding(ITranslatable startPosition, ITranslatable finishPosition);
        Rect GetTeamRect(Team team);
    }
}