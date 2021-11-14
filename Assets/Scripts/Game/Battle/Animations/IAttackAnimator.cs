namespace SecondBreath.Game.Battle.Animations
{
    public interface IAttackAnimator : ICommonCharacterAnimator
    {
        void SetAttackTrigger();
        void SetCast1Trigger();
        void SetCast2Trigger();
        void SetCast3Trigger();
    }
}