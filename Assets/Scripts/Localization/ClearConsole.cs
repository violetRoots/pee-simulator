#if UNITY_EDITOR
using System;
using System.Reflection;
using UnityEditor;

namespace Common.Utils
{
    public static class ClearConsole
    {
        private static MethodInfo _clearConsoleMethod;

        private static MethodInfo ClearConsoleMethod
        {
            get
            {
                if (_clearConsoleMethod == null)
                {
                    Assembly assembly = Assembly.GetAssembly(typeof(SceneView));
                    Type logEntries = assembly.GetType("UnityEditor.LogEntries");
                    _clearConsoleMethod = logEntries.GetMethod("Clear");
                }

                return _clearConsoleMethod;
            }
        }

        public static void ClearLogConsole()
        {
            ClearConsoleMethod.Invoke(new object(), null);
        }
    }
}
#endif