using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttributesDemoScript : MonoBehaviour
{
    [Header("Section 1")] [Range(0, 5)] // See also: Min, Max
    public int rangedInt;

    [Header("Section 2")] [ColorUsage(showAlpha: false, hdr: true)]
    public Color glowColor;

    [TextArea] // See also: Multiline
    public string longText;


    [Demo(5)] public int demoField;


    [Practice("Monday", "Tuesday", "Wednesday", "Thursday", "Friday")]
    public int weekdays;

    public Date date;
}
