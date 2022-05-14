using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZumaController : MonoBehaviour
{
    private Camera mainCamera;

    private void Start()
    {
        mainCamera = Camera.main;
    }

    private void Update()
    {
        ControlFacing();
    }

    private void ControlFacing()
    {
        Vector3 pointerLocation = GetPointerLocation();
        transform.LookAt(pointerLocation);
    }

    public virtual Vector3 GetPointerLocation()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = mainCamera.transform.position.y;

        Vector3 pointerLocation = mainCamera.ScreenToWorldPoint(mousePosition);

        return pointerLocation;
    }
}
