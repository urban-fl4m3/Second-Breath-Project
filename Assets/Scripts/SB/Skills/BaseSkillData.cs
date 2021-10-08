using System;
using SB.Components.Data;
using SB.Skills.Logic;
using TypeReferences;
using UnityEngine;

namespace SB.Skills
{
    public abstract class BaseSkillData : ScriptableObject
    {
        [SerializeField] private string _name;
        [SerializeField] private Sprite _icon;
        [SerializeField] [Inherits(typeof(BaseSkillLogic))] private TypeReference _skillType;
        
        public string Name => _name;
        public Sprite Icon => _icon;
        public Type SkillType => _skillType;
    }
}