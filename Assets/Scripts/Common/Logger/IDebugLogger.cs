namespace SecondBreath.Common.Logger
{
    public interface IDebugLogger
    {
        void Log(string msg);
        void LogError(string msg);
        void LogWarning(string msg);
    }
}