using System;
using Cinemachine;
using Core;
using UniRx;
using UnityEngine;
using Zenject;

namespace Battle
{
    public class BattleSession : MonoBehaviour
    {
        [SerializeField] private CinemachineVirtualCamera _camera;
        [SerializeField] private GameObject _character;

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
        }

        private void OnDestroy()
        {
            _pauseAction?.Dispose();
        }
    }
}