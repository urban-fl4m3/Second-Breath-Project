using Common.Animations;
using UnityEngine;

namespace SecondBreath.Game.Battle.Animations
{
    public class BattleCharacterAnimator : BaseAnimatorController, IMovementAnimator, IAttackAnimator
    {
        [SerializeField] [AnimationParameter(AnimatorControllerParameterType.Bool, nameof(_animator))]
        private string _idleParameter;

        [SerializeField] [AnimationParameter(AnimatorControllerParameterType.Bool, nameof(_animator))]
        private string _runningParameter;

        [SerializeField] [AnimationParameter(AnimatorControllerParameterType.Trigger, nameof(_animator))]
        private string _attackParameter;

        [SerializeField] [AnimationParameter(AnimatorControllerParameterType.Trigger, nameof(_animator))]
        private string _cast1Parameter;

        [SerializeField] [AnimationParameter(AnimatorControllerParameterType.Trigger, nameof(_animator))]
        private string _cast2Parameter;

        [SerializeField] [AnimationParameter(AnimatorControllerParameterType.Trigger, nameof(_animator))]
        private string _cast3Parameter;

        [SerializeField] [AnimationParameter(AnimatorControllerParameterType.Trigger, nameof(_animator))]
        private string _deathParameter;
        
        [SerializeField] [AnimationParameter(AnimatorControllerParameterType.Trigger, nameof(_animator))]
        private string _reviveParameter;

        public bool IsRunning
        {
            get => _isRunning;
            set
            {
                _isRunning = value;
                SetBool(_idleParameter, !_isRunning);
                SetBool(_runningParameter, _isRunning);
            }
        }

        private bool _isRunning;

        public void SetAttackTrigger()
        {
            SetTrigger(_attackParameter);
        }

        public void SetCast1Trigger()
        {
            SetTrigger(_cast1Parameter);
        }

        public void SetCast2Trigger()
        {
            SetTrigger(_cast2Parameter);
        }

        public void SetCast3Trigger()
        {
            SetTrigger(_cast3Parameter);
        }

        public void SetDeathTrigger()
        {
            SetTrigger(_deathParameter);
        }

        public void SetReviveTrigger()
        {
            SetTrigger(_reviveParameter);
        }
    }
}