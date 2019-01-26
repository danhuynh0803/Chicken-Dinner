using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

    public Transform actorToFollow;

    private Vector3 cameraOffset; 

    void Start()
    {
        cameraOffset = transform.position - actorToFollow.transform.position;
    }

    void Update()
    {
        this.transform.position = actorToFollow.transform.position + cameraOffset;
    }
}
