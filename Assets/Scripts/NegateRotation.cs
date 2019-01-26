using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NegateRotation : MonoBehaviour
{
    Quaternion rotation;

    private void Awake()
    {    
        rotation = this.transform.rotation;
    }

    private void LateUpdate()
    {
        this.transform.rotation = rotation;
    }
}
