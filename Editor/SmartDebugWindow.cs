using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class SmartDebugWindow : EditorWindow {

    [SerializeField] public static bool isEnabled;
    [SerializeField] public static bool gameplay;
    [SerializeField] public static bool audio;
    [SerializeField] public static bool save;
    [SerializeField] public static bool design;
    [SerializeField] public static bool userInterface;
    [SerializeField] public static bool variables;
    [SerializeField] public static bool error;
    [SerializeField] public static bool warning;
    [SerializeField] public static bool other;    
    [SerializeField] public static string customFilters;

    [MenuItem("Window/SmartDebug")]
    static void Init()
    {
        InitVars();
        SmartDebugWindow window = (SmartDebugWindow)EditorWindow.GetWindow(typeof(SmartDebugWindow));
        window.titleContent.text = "SmartDebug";

        window.Show();
    }

    private static void InitVars()
    {
        isEnabled = EditorPrefs.GetBool("SmartDebugIsEnabled");

        gameplay = EditorPrefs.GetBool("SmartDebugGameplay");
        audio = EditorPrefs.GetBool("SmartDebugAudio");
        save = EditorPrefs.GetBool("SmartDebugSave");
        design = EditorPrefs.GetBool("SmartDebugDesign");
        userInterface = EditorPrefs.GetBool("SmartDebugUserInterface");
        variables = EditorPrefs.GetBool("SmartDebugVariables");
        error = EditorPrefs.GetBool("SmartDebugError");
        warning = EditorPrefs.GetBool("SmartDebugWarning");
        other = EditorPrefs.GetBool("SmartDebugOther");
        customFilters = EditorPrefs.GetString("SmartDebugCustomFilters");
    }

    private static void SaveChanges()
    {
        EditorPrefs.SetBool("SmartDebugIsEnabled", isEnabled);

        if (isEnabled)
        {
            EditorPrefs.SetBool("SmartDebugGameplay", gameplay);
            EditorPrefs.SetBool("SmartDebugAudio", audio);
            EditorPrefs.SetBool("SmartDebugSave", save);
            EditorPrefs.SetBool("SmartDebugDesign", design);
            EditorPrefs.SetBool("SmartDebugUserInterface", userInterface);
            EditorPrefs.SetBool("SmartDebugVariables", variables);
            EditorPrefs.SetBool("SmartDebugError", error);
            EditorPrefs.SetBool("SmartDebugWarning", warning);
            EditorPrefs.SetBool("SmartDebugOther", other);
            EditorPrefs.SetString("SmartDebugCustomFilters", customFilters);
        }
    }

    private void OnEnable()
    {
        InitVars();
    }

    private void OnDisable()
    {
        SaveChanges();
    }

    private void OnGUI()
    {
        //var style = new GUIStyle();
        //style.normal.textColor = (EditorPrefs.GetBool("SmartDebugIsEnabled") ? new Color(0, 175f / 255f, 46f / 255f) : Color.red);
        //style.fontStyle = FontStyle.Bold;
        //GUILayout.Space(5);
        //GUILayout.Label(" Current Status: " + (EditorPrefs.GetBool("SmartDebugIsEnabled") ? "Enabled" : "Disabled"), style);

        GUILayout.Space(10);
        isEnabled = EditorGUILayout.Toggle("Enable SmartDebug", isEnabled);        

        GUILayout.Space(5);
        if (isEnabled)
        {
            GUILayout.Label("Log messages to show:", EditorStyles.boldLabel);
            gameplay = EditorGUILayout.Toggle("Gameplay", gameplay);
            audio = EditorGUILayout.Toggle("Audio", audio);
            save = EditorGUILayout.Toggle("Save", save);
            design = EditorGUILayout.Toggle("Design", design);
            userInterface = EditorGUILayout.Toggle("User Interface", userInterface);
            variables = EditorGUILayout.Toggle("Variables", variables);
            error = EditorGUILayout.Toggle("Error", error);
            warning = EditorGUILayout.Toggle("Warning", warning);
            customFilters = EditorGUILayout.TextField(new GUIContent("Custom Filters", "Separate them using , and never use spaces!"), customFilters);
        }

        //GUILayout.Space(15);
        //if (GUILayout.Button("Save Changes"))
        //{
        //    SaveChanges();          
        //}

        GUILayout.Space(6);
        EditorGUILayout.HelpBox("SmartDebug uses Debug.Log() to print messages to the console according to what the user want to see (LogFilters). " +
            "Logs are NEVER printed outside the UnityEditor. For logs on your build, use Debug.Log instead. " +
            "You can change what types of log messages you will see on Window/SmartDebug. " +
            "These settings are kept locally using EditorPrefs.", MessageType.Info);
    }
}
