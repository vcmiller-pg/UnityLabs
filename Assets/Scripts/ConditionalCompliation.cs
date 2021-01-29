using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ConditionalCompliation : MonoBehaviour
{
    private void OnDrawGizmos()
    {
        #if UNITY_EDITOR
            Handles.DrawWireCube(
                transform.position, 
                Vector3.one);
        #endif
    }
}
