using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

/// <summary>
/// Uses Debug.Log() to print messages to the console according to what the user want to see (LogFilter).
/// Logs are NEVER printed outside the UnityEditor. For logs on your build, use Debug.Log instead.
/// You can change what types of log messages you will see on Window/SmartDebug.
/// These settings are kept locally using EditorPrefs.
/// </summary>
public static class SmartDebug {

    /// <summary>
    /// Prints the message on your console if the LogFilter is enabled on your machine.
    /// </summary>
    /// <param name="message"></param>
    /// <param name="filter"></param>
    public static void Log(string message, LogFilter filter)
    {
#if UNITY_EDITOR
        if (EditorPrefs.GetBool("SmartDebugIsEnabled") && EditorPrefs.GetBool("SmartDebug" + filter.ToString()))
        {
            Debug.Log(message);
        }
#endif
    }

    /// <summary>
    /// Prints the message on your console if LogFilter.Other is enabled on your machine. 
    /// </summary>
    /// <param name="message"></param>
    public static void Log(string message)
    {
        Log(message, LogFilter.Other);
    }

    /// <summary>
    /// Prints the message on your console if the registered Custom Filters on your machine contain customFilter.
    /// </summary>
    /// <param name="message"></param>
    /// <param name="customFilter"></param>
    public static void Log(string message, string customFilter)
    {
#if UNITY_EDITOR
        if (EditorPrefs.GetBool("SmartDebugIsEnabled")) {
            string[] filters = EditorPrefs.GetString("SmartDebugCustomFilters").Split(',');
            for (int i = 0; i < filters.Length; i++)
            {
                if (filters[i] == customFilter)
                {
                    Debug.Log(message);
                    return;
                }
            }
        }        
#endif
    }

    /// <summary>
    /// Prints the message on your console if the LogFilter is enabled on your machine AND if the registered Custom Filters on your machine contain customFilter.
    /// </summary>
    /// <param name="message"></param>
    /// <param name="filter"></param>
    /// <param name="customFilter"></param>
    public static void Log(string message, LogFilter filter, string customFilter)
    {
#if UNITY_EDITOR
        if (EditorPrefs.GetBool("SmartDebugIsEnabled") && EditorPrefs.GetBool("SmartDebug" + filter.ToString()))
        {
            Log(message, customFilter);
        }
#endif
    }

    public enum LogFilter
    {
        Gameplay,
        Audio,
        Save,
        Design,
        UserInterface,
        Variables,
        Error = 97,
        Warning = 98,
        Other = 99
    }
}
