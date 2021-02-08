using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

//[CustomEditor(typeof(UnlockArea))]
public class PracticeUnlockAreaEditor : Editor
{
    private UnlockArea _unlockArea;
    private readonly string[] _excludePropertiesWithValues = {
        nameof(UnlockArea.areaSize), nameof(UnlockArea.containedSquares), "m_Script"
    };
    private readonly string[] _excludeProperties = {
        nameof(UnlockArea.areaSize), "m_Script"
    };
    
    private void OnEnable()
    {
        _unlockArea = (UnlockArea) target;
    }

    public override void OnInspectorGUI()
    {
        var areaSizeProperty = serializedObject.FindProperty(nameof(_unlockArea.areaSize));
        Vector2Int newSize = EditorGUILayout.Vector2IntField(areaSizeProperty.displayName, _unlockArea.areaSize);
        newSize.x = Mathf.Max(newSize.x, 1);
        newSize.y = Mathf.Max(newSize.y, 1);
        if (newSize != _unlockArea.areaSize)
        {
            Undo.RecordObject(_unlockArea, "Resize Area");
            PrefabUtility.RecordPrefabInstancePropertyModifications(_unlockArea);
            _unlockArea.Resize(newSize);
        }
        
        serializedObject.Update();
        
        GUILayout.BeginHorizontal();
        if (GUILayout.Button("Enable All"))
        {
            SetAllSquaresEnabled(true);
        }
        if (GUILayout.Button("Disable All"))
        {
            SetAllSquaresEnabled(false);
        }
        GUILayout.EndHorizontal();

        bool showValues = EditorPrefs.GetBool("UnlockAreaValues");
        DrawPropertiesExcluding(serializedObject, showValues ? _excludeProperties : _excludePropertiesWithValues);
        serializedObject.ApplyModifiedProperties();
    }

    private void OnSceneGUI()
    {
        Vector3 posRounded = Vector3Int.RoundToInt(_unlockArea.transform.position);
        
        RectInt rect = _unlockArea.AreaBounds;
        rect.position = Vector2Int.zero;
        foreach (Vector2Int position in rect.allPositionsWithin)
        {
            Vector3 pos3d = posRounded + new Vector3(position.x, 0, position.y);
            bool isActive = _unlockArea.IsPositionEnabled(position);
            Handles.color = isActive ? Color.green : Color.red;
            if (Handles.Button(pos3d, Quaternion.LookRotation(Vector3.up), 0.4f, 0.4f, Handles.RectangleHandleCap))
            {
                Undo.RecordObject(_unlockArea, "Toggle Position");
                PrefabUtility.RecordPrefabInstancePropertyModifications(_unlockArea);
                _unlockArea.TogglePosition(position);
            }
        }
    }

    private void SetAllSquaresEnabled(bool value)
    {
        Undo.RecordObject(_unlockArea, "Set All Cells");
        PrefabUtility.RecordPrefabInstancePropertyModifications(_unlockArea);
        _unlockArea.SetAllEnabled(value);
    }
}
