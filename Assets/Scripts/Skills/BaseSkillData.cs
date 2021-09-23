using System;
using Components.Data;
using Skills.Logic;
using TypeReferences;
using UnityEngine;

namespace Skills
{
    public abstract class BaseSkillData : ScriptableObject
    {
        [SerializeField] private string _name;
        [SerializeField] private Sprite _icon;
        [SerializeField] [Inherits(typeof(BaseSkillLogic))] private TypeReference _skillType;
        
        public string Name => _name;
        public Sprite Icon => _icon;
        public Type SkillType => _skillType;
        
        public abstract DataModel GetSkillData();
    }
}