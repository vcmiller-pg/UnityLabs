using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public static class PracticePreferenceItem
{
    [SettingsProvider]
    public static SettingsProvider CreateCustomUserSettingsProvider()
    {
        return new SettingsProvider(
            "Preferences/Custom User Settings", 
            SettingsScope.User)
        {
            guiHandler = s =>
            {
                float lWidth = EditorGUIUtility.labelWidth;
                EditorGUIUtility.labelWidth = 350;
                EditorGUILayout.LabelField("This is a custom prefs menu.", EditorStyles.boldLabel);

                bool curValue = EditorPrefs.GetBool("UnlockAreaValues", false);
                bool newValue = EditorGUILayout.Toggle("Show UnlockArea values in inspector.", curValue);
                if (curValue != newValue)
                {
                    EditorPrefs.SetBool("UnlockAreaValues", newValue);
                }

                EditorGUIUtility.labelWidth = lWidth;
            }
        };
    }
}