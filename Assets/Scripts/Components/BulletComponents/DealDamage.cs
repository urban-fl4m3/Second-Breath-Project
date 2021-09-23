﻿using Components.Data;
using UnityEngine;

namespace Components.BulletComponents
{
    public class DealDamage : MonoBehaviour
    {
        [SerializeField] private float damage = 10.0f;
        private void OnCollisionEnter2D(Collision2D other)
        {
            var targetObject = other.gameObject;
            if (targetObject.GetComponent<Battle.Character>() == null) return;
            DataModel targetData = targetObject.GetComponent<Battle.Character>().characterData.Properties;
            targetData.GetProperty<float>(Attributes.CurrentHealth).Value -= damage;
        }
    }
}