namespace Common.Actors
{
    public interface IActor
    {
        IReadOnlyComponentContainer Components { get; }
        
        void Enable();
        void Disable();
    }
}