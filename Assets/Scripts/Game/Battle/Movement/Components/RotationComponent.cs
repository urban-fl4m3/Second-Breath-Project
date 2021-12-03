using Common.Actors;
using UnityEngine;

namespace SecondBreath.Game.Battle.Movement.Components
{
    public class RotationComponent : ActorComponent
    {
        public void LookAt(ITranslatable translatable)
        {
            var direction = translatable.Position.Value - transform.position;
            var rotation = Quaternion.LookRotation(direction, Vector3.up);
            
            transform.rotation = rotation;
        }
    }
}