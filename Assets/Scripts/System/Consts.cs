using UnityEngine;

namespace Constants
{
    /// <summary> 定数管理ファイル </summary>
    public static class Consts
    {
        #region Console Logs
        public static void Log(object message)
        {
#if UNITY_EDITOR
            Debug.Log(message);
#endif
        }

        public static void LogWarning(object message)
        {
#if UNITY_EDITOR
            Debug.LogWarning(message);
#endif
        }

        public static void LogError(object message)
        {
#if UNITY_EDITOR
            Debug.LogError(message);
#endif
        }
        #endregion
    }
}
