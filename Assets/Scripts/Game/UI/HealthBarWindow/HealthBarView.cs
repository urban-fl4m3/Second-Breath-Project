using System;
using Common.Actors;
using SecondBreath.Game.Battle.Damage;
using SecondBreath.Game.Battle.Movement;
using UnityEngine;
using UnityEngine.UI;
using UniRx;

namespace SecondBreath.Game.UI
{
    public class HealthBarView : MonoBehaviour, IDisposable
    {
        [SerializeField] private Image _fillImage;
        
        private IHealable _healable;
        private ITranslatable _translatable;
        private IDisposable _positionChanged;
        
        public void TrackActor(IActor actor)
        {
            _healable = actor.Components.Get<IHealable>();
            _translatable = actor.Components.Get<ITranslatable>();
            
            _healable.Health.HealthRemained += HandleHealthRemained;
            _positionChanged = _translatable.Position.Subscribe(OnPositionChanged);
            
            OnPositionChanged(_translatable.Position.Value);
            OnHealthChanged(_healable.Health.CurrentHealth);
        }

        public void Dispose()
        {
            _healable.Health.HealthRemained -= HandleHealthRemained;
            _positionChanged?.Dispose();
        }

        private void HandleHealthRemained(object sender, float newHealth)
        {
            OnHealthChanged(newHealth);
        }

        private void OnHealthChanged(float newHealth)
        {
            _fillImage.fillAmount = newHealth / _healable.Health.MaximumHealth;
        }

        private void OnPositionChanged(Vector3 newPosition)
        {
            //todo add camera provider and bind it
            newPosition.y += _translatable.Height;
            transform.position = Camera.main.WorldToScreenPoint(newPosition);
        }
    }
}