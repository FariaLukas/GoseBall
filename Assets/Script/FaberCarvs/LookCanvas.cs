using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookCanvas : MonoBehaviour
{
    private Camera mainCamera;
    public bool lockRotationX;
    public bool lockRotationY;
    public bool lockRotationZ;
    private Vector3 startRotation;

    private void OnEnable()
    {
        mainCamera = FindObjectOfType<Camera>();
        startRotation = transform.rotation.eulerAngles;
    }

    private void Update()
    {
        Vector3 targetPos = mainCamera.transform.position;
        targetPos.x = lockRotationX ? transform.position.x : targetPos.x;
        targetPos.y = lockRotationY ? transform.position.y : targetPos.y;
        targetPos.z = lockRotationZ ? transform.position.z : targetPos.z;

        transform.LookAt(targetPos);
    }
}
