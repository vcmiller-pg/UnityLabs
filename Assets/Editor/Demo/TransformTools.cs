using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.ShortcutManagement;
using UnityEngine;

public static class TransformTools
{
    [Shortcut("3D Viewport/Apply Position", KeyCode.T, ShortcutModifiers.Action | ShortcutModifiers.Shift)]
    [MenuItem("Tools/Apply Position")]
    public static void ApplyPosition()
    {
        foreach (GameObject selected in Selection.gameObjects)
        {
            Undo.RecordObject(selected.transform, "Apply Position");
            PrefabUtility.RecordPrefabInstancePropertyModifications(selected.transform);
            Vector3 offset = Quaternion.Inverse(selected.transform.localRotation) * selected.transform.localPosition;
            selected.transform.localPosition = Vector3.zero;

            foreach (Transform child in selected.transform)
            {
                Undo.RecordObject(child, "Apply Position");
                PrefabUtility.RecordPrefabInstancePropertyModifications(child);
                child.localPosition += offset;
            }
        }
    }
}