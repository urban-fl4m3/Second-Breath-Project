namespace SecondBreath.Game.Ticks
{
    public interface IGameTickHandler : IGameTickCollection
    {
        void StartTicking();
        void StopTicking();
    }
}