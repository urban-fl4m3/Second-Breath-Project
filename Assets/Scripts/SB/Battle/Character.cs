using System;
using System.Collections.Generic;
using SB.Character;
using SB.Components.Data;
using SB.Core;
using SB.Helpers;
using SB.Skills;
using SB.Skills.Logic;
using UniRx;
using UnityEngine;
using Zenject;

namespace SB.Battle
{
    public class Character : ExtendedMonoBehaviour
    {
        [Inject] private DiContainer _container;

        [SerializeField] private List<BaseSkillData> _skillsData;
        private List<BaseSkillLogic> _skillsLogic = new List<BaseSkillLogic>();

        [HideInInspector] public DataHolder characterData;
        private float _currentHealth = 50.0f;
        public ReactiveProperty<Weapon> _weapon;

        private IDisposable _handPosUpdater;
        private IDisposable _weaponUpdater;

        public void Init()
        {
            gameObject.ActivateGameComponents();
            InitCharacter();
            InitSkills();
        }

        public void InitCharacter()
        {
            characterData = this.GetOrAddComponent<DataHolder>();
            characterData.Properties.AddProperty(Attributes.CurrentHealth, _currentHealth);
            _handPosUpdater = characterData.Properties.GetOrCreateProperty<Transform>(Attributes.HandTransform).AsObservable().Subscribe(UpdateWeaponParent);
            _weaponUpdater = _weapon.AsObservable().Subscribe(WeaponWasUpdated);
        }


        private void UpdateWeaponParent(Transform newValue)
        {
            _weapon.Value?.transform.SetParent(newValue);
        }

        private void WeaponWasUpdated(Weapon newValue)
        {
            if (!characterData.Properties.ContainsValue<Transform>(Attributes.HandTransform) || newValue == null) return;
            var newParent = characterData.Properties.GetProperty<Transform>(Attributes.HandTransform);
            newValue.transform.SetParent(newParent.Value);
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
            _weapon.Value = dataModel.GetProperty<Weapon>(Attributes.Weapon).Value;

        }

        private void OnDestroy()
        {
            _handPosUpdater?.Dispose();
            _weaponUpdater?.Dispose();
        }
    }
}