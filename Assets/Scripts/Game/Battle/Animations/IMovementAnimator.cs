namespace SecondBreath.Game.Battle.Animations
{
    public interface IMovementAnimator : ICommonCharacterAnimator
    {
        public bool IsRunning { get; set; }
    }
}