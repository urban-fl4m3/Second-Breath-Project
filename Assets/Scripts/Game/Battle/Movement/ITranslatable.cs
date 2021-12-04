using UniRx;
using UnityEngine;

namespace SecondBreath.Game.Battle.Movement
{
    public interface ITranslatable
    {
        IReadOnlyReactiveProperty<Vector3> Position { get; }
        float Radius { get; }
        float Height { get; }
    }
}