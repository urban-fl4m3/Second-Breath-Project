namespace Common.Actors
{
    public interface IReadOnlyComponentContainer
    {
        IActor Owner { get; }

        T Get<T>();
    }
}