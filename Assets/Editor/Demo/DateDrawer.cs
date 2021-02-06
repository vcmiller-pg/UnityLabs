using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(Date))]
public class DateDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        Rect valueRect = EditorGUI.PrefixLabel(position, label);

        SerializedProperty dayProperty = property.FindPropertyRelative(nameof(Date.day));
        SerializedProperty monthProperty = property.FindPropertyRelative(nameof(Date.month));
        SerializedProperty yearProperty = property.FindPropertyRelative(nameof(Date.year));

        Rect monthRect = valueRect;
        monthRect.width /= 2;

        Rect dayRect = valueRect;
        dayRect.width /= 4;
        dayRect.x = monthRect.xMax;

        Rect yearRect = valueRect;
        yearRect.width /= 4;
        yearRect.x = dayRect.xMax;

        int daysInMonth = Date.DaysInMonth((Month) monthProperty.enumValueIndex, yearProperty.intValue);
        
        EditorGUI.PropertyField(monthRect, monthProperty, GUIContent.none);
        dayProperty.intValue = Mathf.Clamp(EditorGUI.IntField(dayRect, GUIContent.none, dayProperty.intValue), 1, daysInMonth);
        yearProperty.intValue = Mathf.Max(EditorGUI.IntField(yearRect, GUIContent.none, yearProperty.intValue), 0);
    }
}
