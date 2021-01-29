using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(DemoAttribute))]
public class DemoAttributeDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, 
        SerializedProperty property, GUIContent label)
    {
        
    }
}
