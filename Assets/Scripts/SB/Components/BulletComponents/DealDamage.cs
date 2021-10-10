using SB.Battle;
using SB.Common.Attributes;
using SB.Core;
using UnityEngine;

namespace SB.Components
{
    public class DealDamage : GameComponent
    {
        [SerializeField] private float damage = 10.0f;
        
        private void OnCollisionEnter2D(Collision2D other)
        {
            var targetObject = other.gameObject;
            if (targetObject.GetComponent<Character>() == null) return;
            
            var attributes = targetObject.GetComponent<Character>().Attributes;
            attributes[AttributeType.Health].Value -= damage;
        }
    }
}