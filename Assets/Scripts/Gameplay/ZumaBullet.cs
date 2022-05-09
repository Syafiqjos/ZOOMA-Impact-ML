using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZumaBullet : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;

    private bool placedFlag = false;

    public void LaunchBullet(Vector3 direction, float speed)
    {
        rb.AddForce(direction * speed, ForceMode.Impulse);
    }

    public bool GetPlacedFlag()
    {
        return placedFlag;
    }

    public void SetPlacedFlag(bool flag)
    {
        placedFlag = flag;
    }
}
