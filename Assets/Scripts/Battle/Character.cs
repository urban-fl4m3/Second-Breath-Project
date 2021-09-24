using System;
using System.Collections.Generic;
using Character;
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
        private ReactiveProperty<Weapon> _weapon;

        private IDisposable _handPosUpdater;

        public void Init()
        {
            InitCharacter();
            InitSkills();
        }

        public void InitCharacter()
        {
            characterData = this.GetOrAddComponent<DataHolder>();
            characterData.Properties.AddProperty(Attributes.CurrentHealth, _currentHealth);
            _handPosUpdater = characterData.Properties.GetOrCreateProperty<Transform>(Attributes.HandTransform).AsObservable().Subscribe(UpdateWeaponParent);
        }


        private void UpdateWeaponParent(Transform newValue)
        {
            if (_weapon == null) return;
            _weapon.Value.transform.SetParent(newValue);
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
            characterData.Properties.AddProperty(Attributes.MaxHealth, dataModel.GetOrCreateProperty<float>(Attributes.MaxHealth).Value);
            _weapon = dataModel.GetProperty<Weapon>(Attributes.Weapon);
        }

        private void OnDestroy()
        {
            
        }
    }
}