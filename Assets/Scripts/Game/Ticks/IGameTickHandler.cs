namespace SecondBreath.Game.Ticks
{
    public interface IGameTickHandler : IGameTickWriter
    {
        void StartTicking();
        void StopTicking();
    }
}