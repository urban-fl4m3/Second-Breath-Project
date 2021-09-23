using System;
using UnityEngine;

namespace Skills
{
    [Serializable]
    public struct AttributeDataModel
    {
        [SerializeField] private Attributes _attribute;
        [SerializeField] private float _value;

        public Attributes Attribute => _attribute;
        public float Value => _value;
    }
}