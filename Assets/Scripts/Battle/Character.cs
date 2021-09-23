using System;
using System.Collections.Generic;
using Cinemachine;
using Components.Data;
using Core;
using Helpers;
using Skills;
using Skills.Logic;
using UniRx;
using UnityEngine;
using Zenject;

namespace Battle
{
    public class Character : ExtendedMonoBehaviour
    {
        [Inject] private DiContainer _container;

        [SerializeField] private List<BaseSkillData> _skillsData;
        private List<BaseSkillLogic> _skillsLogic = new List<BaseSkillLogic>();

        public DataHolder characterData;
        private float _currentHealth = 50.0f;

        public void Init()
        {
            InitCharacter();
            InitSkills();
        }

        public void InitCharacter()
        {
            characterData = this.GetOrAddComponent<DataHolder>();
            characterData.Properties.AddProperty(Attributes.CurrentHealth, _currentHealth);
        }

        private void InitSkills()
        {
            foreach (var skillData in _skillsData)
            {
                var data = skillData.GetDataModel();
                var newSkill = (BaseSkillLogic)_container.Instantiate(skillData.SkillType);
                newSkill.SetData(data);
                newSkill.Activate(gameObject);
                _skillsLogic.Add(newSkill);
            }
        }

        public void SetData(DataModel dataModel)
        {
            characterData.Properties.AddProperty(Attributes.MaxHealth, dataModel.GetProperty<float>(Attributes.MaxHealth).Value);
        }

        private void OnDestroy()
        {
            
        }
    }
}