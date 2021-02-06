using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(IntMaskAttribute))]
public class IntMaskAttributeDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        if (property.propertyType != SerializedPropertyType.Integer)
        {
            EditorGUI.LabelField(position, label, new GUIContent("Cannot use IntMask with a variable of this type."));
            return;
        }
        
        var attr = (IntMaskAttribute)attribute;
        property.intValue = EditorGUI.MaskField(position, label, property.intValue, attr.Options);
    }
}
