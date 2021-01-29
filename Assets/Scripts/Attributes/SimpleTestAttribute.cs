using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemoAttribute : PropertyAttribute
{
    public readonly int Value;
    
    public DemoAttribute(int value)
    {
        Value = value;
    }
}
