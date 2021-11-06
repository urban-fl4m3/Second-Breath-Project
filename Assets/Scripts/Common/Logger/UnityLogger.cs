using UnityEngine;

namespace SecondBreath.Common.Logger
{
    public class UnityLogger : ILogger
    {
        public void Log(string msg)
        {
            Debug.Log(msg);
        }

        public void LogError(string msg)
        {
            Debug.LogError(msg);
        }

        public void LogWarning(string msg)
        {
            Debug.LogWarning(msg);
        }
    }
}