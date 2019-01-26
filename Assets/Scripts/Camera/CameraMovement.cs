using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour {

    public float cameraZoomSpeed;
    public float cameraScrollXMin;
    public float cameraScrollYMin;
    public float cameraScrollXMax;
    public float cameraScrollYMax;
    public float cameraScrollSpeedX;
    public float cameraScrollSpeedY;
    public float[] zoomLazyerZ;
    public int zoomIndex;
    private Transform lockOnTargetTrnasform;
    private float scrollX;
    private float scrollY;
    private Vector3 mousePositionPixel;
    private Vector3 mousePositionToViewPort;
    new private Transform camera;

    private void Start()
    {
        zoomIndex = 5;
        camera = this.transform;
    }


    private void Update()
    {
        float keyInputX = Input.GetAxisRaw("Horizontal");
        float keyInputY = Input.GetAxisRaw("Vertical");
        //Input.GetAxis("Mouse ScrollWheel") = -0.1, 0, or 0.1
        float scrollWheel = Input.GetAxis("Mouse ScrollWheel");
        mousePositionToViewPort = Camera.main.ScreenToViewportPoint(Input.mousePosition);
        if (mousePositionCheck(mousePositionToViewPort.x, cameraScrollXMin, cameraScrollXMax))
            scrollX = Mathf.Sign(mousePositionToViewPort.x - 0.5f);
        else
        if (keyInputX != 0f)
            scrollX = Mathf.Sign(keyInputX);
        else
            scrollX = 0f;
        if (mousePositionCheck(mousePositionToViewPort.y, cameraScrollYMin, cameraScrollYMax))
            scrollY = Mathf.Sign(mousePositionToViewPort.y - 0.5f);
        else
        if (keyInputY != 0f)
            scrollY = Mathf.Sign(keyInputY);
        else
        scrollY = 0f;

        transform.position += new Vector3(scrollX * cameraScrollSpeedX * Time.deltaTime, scrollY * cameraScrollSpeedX * Time.deltaTime, 0f);
        zoomIndex = (int)Mathf.Clamp((zoomIndex + Input.GetAxis("Mouse ScrollWheel") * 10f), 0f, zoomLazyerZ.Length - 1);

        if (IsZooming())
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, zoomLazyerZ[zoomIndex]);
        }
        else
        {
            float sign = Mathf.Sign(zoomLazyerZ[zoomIndex] - transform.position.z);
            float cameraZoom = sign * cameraZoomSpeed * Time.deltaTime;
            transform.position += new Vector3(0f, 0f, cameraZoom);
        }

        if(lockOnTargetTrnasform != null)
        {
            if(Input.GetKey(KeyCode.Mouse1))
            {
                lockOnTargetTrnasform = null;
                return;
            }
            transform.position = new Vector3(lockOnTargetTrnasform.position.x, lockOnTargetTrnasform.position.y, transform.position.z);
        }
    }

    private bool mousePositionCheck(float input, float min, float max)
    {
        if ((input < min || input > max) && input >= 0f && input <=1f)
            return true;
        return false;
    }

    private bool IsZooming()
    {
        if (Mathf.Abs(zoomLazyerZ[zoomIndex] - transform.position.z) < 0.1f)
            return true;
        return false;
    }

    public void LockOnTarget(Transform target)
    {
        lockOnTargetTrnasform = target;
    }
}
