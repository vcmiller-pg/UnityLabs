using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CallbacksDemo : MonoBehaviour
{
    public int dynamicInt;
    public int dynamicIntMin;
    public int dynamicIntMax;

    public Transform valueIndicator;

    private void OnValidate()
    {
        dynamicInt = Mathf.Clamp(
            dynamicInt, dynamicIntMin, dynamicIntMax);

        valueIndicator.localPosition = 
            Vector3.up * Mathf.InverseLerp(
                dynamicIntMin, dynamicIntMax, dynamicInt);
    }

    public new Rigidbody rigidbody;
    public GameObject bulletPrefab;
    
    private void Reset()
    {
        rigidbody = GetComponent<Rigidbody>();
        bulletPrefab = Resources.Load<GameObject>("Prefabs/Bullet");
    }


    public Vector3[] spawnPoints;
    private void OnDrawGizmos()
    {
        foreach (Vector3 point in spawnPoints)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawCube(point, 
                new Vector3(0.2f, 0.5f, 0.2f));
        }
    }
}
