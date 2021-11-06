using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace SecondBreath.Game.Stats
{
    [Serializable]
    public struct StatData
    {
        [SerializeField] private float _value;
        [SerializeField] private bool _isUpgradeable;
        [SerializeField] [ShowIf("_isUpgradeable")] private int _tier;

        public float Value => _value;
        public bool IsUpgradeable => _isUpgradeable;
        public int Tier => _tier;
    }
}