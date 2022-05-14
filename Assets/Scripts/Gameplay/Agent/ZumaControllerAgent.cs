using UnityEngine;
using System.Collections;

public class ZumaControllerAgent : ZumaController
{
    [SerializeField] private Transform pointer;

    public override Vector3 GetPointerLocation()
    {
        return pointer.position;
    }
}
