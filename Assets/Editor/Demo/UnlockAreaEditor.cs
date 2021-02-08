using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(UnlockArea))]
public class UnlockAreaEditor : Editor
{
    private UnlockArea _unlockArea;

    public const string ShowValuesKey = "ShowUnlockAreaValues";

    private void OnEnable()
    {
        _unlockArea = (UnlockArea) target;
    }

    public override void OnInspectorGUI()
    {
        SerializedProperty areaSizeProperty = serializedObject.FindProperty(nameof(UnlockArea.areaSize));
        Vector2Int newSize = EditorGUILayout.Vector2IntField(areaSizeProperty.displayName, _unlockArea.areaSize);
        if (newSize != _unlockArea.areaSize)
        {
            Undo.RecordObject(_unlockArea, "Resize Area");
            PrefabUtility.RecordPrefabInstancePropertyModifications(_unlockArea);
            _unlockArea.Resize(newSize);
        }
        
        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("Enable All"))
        {
            SetAllEnabled(true);
        }
        if (GUILayout.Button("Disable All"))
        {
            SetAllEnabled(false);
        }
        EditorGUILayout.EndHorizontal();
        
        serializedObject.Update();
        if (EditorPrefs.GetBool(ShowValuesKey, false))
        {
            DrawPropertiesExcluding(serializedObject, nameof(UnlockArea.areaSize), "m_Script");
        }
        else
        {
            DrawPropertiesExcluding(serializedObject, nameof(UnlockArea.areaSize), "m_Script", nameof(UnlockArea.containedSquares));
        }
        serializedObject.ApplyModifiedProperties();
    }

    private void OnSceneGUI()
    {
        foreach (Vector2Int position in _unlockArea.LocalAreaBounds.allPositionsWithin)
        {
            Vector3 worldPos = new Vector3(position.x, 0, position.y);
            Handles.color = _unlockArea.IsPositionEnabled(position) ? Color.green : Color.red;
            if (Handles.Button(worldPos, Quaternion.LookRotation(Vector3.up), 0.4f, 0.4f, Handles.RectangleHandleCap))
            {
                Undo.RecordObject(_unlockArea, "Toggle Position");
                PrefabUtility.RecordPrefabInstancePropertyModifications(_unlockArea);
                _unlockArea.TogglePosition(position);
            }
        }
    }

    private void SetAllEnabled(bool value)
    {
        Undo.RecordObject(_unlockArea, "Set All Enabled");
        PrefabUtility.RecordPrefabInstancePropertyModifications(_unlockArea);
        _unlockArea.SetAllEnabled(value);
    }
}
