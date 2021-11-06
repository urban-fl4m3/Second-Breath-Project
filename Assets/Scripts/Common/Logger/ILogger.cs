namespace SecondBreath.Common.Logger
{
    public interface ILogger
    {
        void Log(string msg);
        void LogError(string msg);
        void LogWarning(string msg);
    }
}