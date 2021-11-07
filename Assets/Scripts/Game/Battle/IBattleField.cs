using UnityEngine;

namespace SecondBreath.Game.Battle
{
    public interface IBattleField
    {
        Plane GetPlane();

        bool InFriendlyField(Vector2 point);
        bool InEnemyField(Vector2 point);
    }
}