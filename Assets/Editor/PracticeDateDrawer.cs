using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

//[CustomPropertyDrawer(typeof(Date))]
public class PracticeDateDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        Rect valueRect = EditorGUI.PrefixLabel(position, label);

        Rect monthSpace = valueRect;
        monthSpace.width /= 2;
        monthSpace.width -= 2;

        Rect daySpace = valueRect;
        daySpace.width /= 4;
        daySpace.width -= 4;
        daySpace.x = monthSpace.xMax + 2;

        Rect yearSpace = daySpace;
        yearSpace.x = daySpace.xMax + 2;

        SerializedProperty monthProperty = property.FindPropertyRelative(nameof(Date.month));
        SerializedProperty dayProperty = property.FindPropertyRelative(nameof(Date.day));
        SerializedProperty yearProperty = property.FindPropertyRelative(nameof(Date.year));

        EditorGUI.PropertyField(monthSpace, monthProperty, GUIContent.none);

        dayProperty.intValue =
            Mathf.Clamp(EditorGUI.IntField(daySpace, GUIContent.none, dayProperty.intValue),
                0,
                Date.DaysInMonth((Month) monthProperty.enumValueIndex, yearProperty.intValue));

        yearProperty.intValue =
            Mathf.Max(EditorGUI.IntField(yearSpace, GUIContent.none, yearProperty.intValue), 0);
    }
}
