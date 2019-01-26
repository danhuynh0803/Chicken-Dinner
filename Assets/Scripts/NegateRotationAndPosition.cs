using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NegateRotationAndPosition : MonoBehaviour
{
    Vector3 position;
    Quaternion rotation;   

    private void Awake()
    {
        position = transform.position;
        rotation = transform.rotation;
    }

    private void LateUpdate()
    {
        transform.position = position;
        transform.rotation = rotation;
    }
}
