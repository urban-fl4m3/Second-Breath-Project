using SecondBreath.Common.Ticks;
using UnityEngine;

namespace SecondBreath.Game.Battle.Movement
{
    public class MovementUpdate : ITickUpdate
    {
        private readonly Transform _transform;
        private readonly float _movementSpeed;

        public MovementUpdate(Transform transform, float movementSpeed)
        {
            _transform = transform;
            _movementSpeed = movementSpeed;
        }
        
        public void Update()
        {
            _transform.position += _transform.forward * _movementSpeed * Time.deltaTime;
        }
    }
}