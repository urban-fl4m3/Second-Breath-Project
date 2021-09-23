using System;
using Cinemachine;
using Core;
using Skills;
using Skills.Logic;
using UniRx;
using UnityEngine;
using Zenject;

namespace Battle
{
    public class BattleSession : ExtendedMonoBehaviour
    {
        [SerializeField] private CinemachineVirtualCamera _camera;
        [SerializeField] private GameObject _character;
        [SerializeField] private BaseSkillData _baseSkillData;
        
        [Inject] private DiContainer _container;
        [Inject] private RegistrationMap _registrationMap;
        [Inject] private PauseController _pauseController;

        private IDisposable _pauseAction;
        
        private void Start()
        {
            _pauseAction = Observable.EveryUpdate().Where(_ => Input.GetKeyDown(KeyCode.Space)).Subscribe(_ =>
            {
                _pauseController.ChangePauseState();
            });
            
            ReadFromConfig();
            UpgradeStats();
            SpawnCharacter();
        }

        private void ReadFromConfig()
        {
            
        }

        private void UpgradeStats()
        {
        }

        private void SpawnCharacter()
        {
            var character = _container.InstantiatePrefab(_character);
            _registrationMap.RegisterObject(character);
            _camera.Follow = character.transform;

            AddSkill(character, _baseSkillData);
        }

        private void AddSkill(GameObject owner, BaseSkillData skillData)
        {
            var data = skillData.GetSkillData();

            var a = (BaseSkillLogic)_container.Instantiate(skillData.SkillType);
            a.SetData(data);   
            a.Activate(owner);
        }
        
        private void OnDestroy()
        {
            _pauseAction?.Dispose();
        }
    }
}