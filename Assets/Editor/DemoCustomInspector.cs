using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(AttributesDemoScript))]
public class DemoCustomInspector : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
    }

    private void OnSceneGUI()
    {
        var transform = ((Component) target).transform;
        if (Handles.Button(
            transform.position, transform.rotation, 
            1.0f, 1.0f, Handles.CubeHandleCap))
        {
            Debug.Log("Button Clicked!", target);
        }
    }
}
