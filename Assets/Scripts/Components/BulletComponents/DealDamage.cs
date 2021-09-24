using Components.Data;
using Core;
using UnityEngine;

namespace Components.BulletComponents
{
    public class DealDamage : GameComponent
    {
        [SerializeField] private float damage = 10.0f;
        private void OnCollisionEnter2D(Collision2D other)
        {
            var targetObject = other.gameObject;
            if (targetObject.GetComponent<Battle.Character>() == null) return;
            
            DataModel targetData = targetObject.GetComponent<Battle.Character>().characterData.Properties;
            targetData.GetOrCreateProperty<float>(Attributes.CurrentHealth).Value -= damage;
        }
    }
}