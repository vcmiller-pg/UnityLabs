using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PracticeAttribute : PropertyAttribute
{
    public string[] Options { get; }
    
    public PracticeAttribute(params string[] options)
    {
        Debug.Assert(options != null && options.Length > 0);
        Options = options;
    }
}
