using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public static class CustomPreferenceItem
{
    [SettingsProvider]
    public static SettingsProvider MyCustomSettings()
    {
        return new SettingsProvider("Preferences/My Tool", SettingsScope.User)
        {
            guiHandler = s =>
            {
                bool curValue = EditorPrefs.GetBool(UnlockAreaEditor.ShowValuesKey, false);
                bool newValue = EditorGUILayout.Toggle("Show UnlockArea Array", curValue);
                if (curValue != newValue)
                {
                    EditorPrefs.SetBool(UnlockAreaEditor.ShowValuesKey, newValue);
                }
            }
        };
    }
}
