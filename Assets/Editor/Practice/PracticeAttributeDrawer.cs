using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(PracticeAttribute))]
public class PracticeAttributeDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        PracticeAttribute attr = (PracticeAttribute) attribute;
        if (property.propertyType != SerializedPropertyType.Integer)
        {
            EditorGUI.LabelField(position, label, new GUIContent($"Field is not an integer."));
        }

        property.intValue = EditorGUI.MaskField(position, label, property.intValue, attr.Options);
    }
}
