using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditor.ShortcutManagement;
using UnityEngine;

public static class PracticeApplyPosition
{
    //[MenuItem("Tools/Apply Position")]
    //[Shortcut("3D Viewport/Apply Transform Position", KeyCode.T,ShortcutModifiers.Action | ShortcutModifiers.Shift)]
    public static void ApplyPosition()
    {
        foreach (GameObject gameObject in Selection.gameObjects)
        {
            Undo.RecordObject(gameObject.transform, "Apply Position");
            PrefabUtility.RecordPrefabInstancePropertyModifications(gameObject.transform);
            
            Vector3 position = gameObject.transform.localPosition;
            Vector3 childOffset = Quaternion.Inverse(gameObject.transform.localRotation) * position;
            gameObject.transform.localPosition = Vector3.zero;

            foreach (Transform child in gameObject.transform)
            {
                Undo.RecordObject(child.transform, "Apply Position");
                PrefabUtility.RecordPrefabInstancePropertyModifications(child.transform);
                child.transform.localPosition += childOffset;
            }
        }
    }
}
