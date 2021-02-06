using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntMaskAttribute : PropertyAttribute
{
    public string[] Options { get; }

    public IntMaskAttribute(params string[] options)
    {
        Options = options;
    }
}