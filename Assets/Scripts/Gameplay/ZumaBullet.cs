using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZumaBullet : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;

    public void LaunchBullet(Vector3 direction, float speed)
    {
        rb.AddForce(direction * speed, ForceMode.Impulse);
    }
}
