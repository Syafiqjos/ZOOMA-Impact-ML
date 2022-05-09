using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZumaShooter : MonoBehaviour
{
    [SerializeField] private bool isEnable = true;
    [SerializeField] private float bulletSpeed = 1f;

    [SerializeField] private ZumaController controller;
    [SerializeField] private ZumaBullet bulletPrefab;
    [SerializeField] private OrbGenerator orbGenerator;

    private void Update()
    {
        CheckShooting();
    }

    private void CheckShooting()
    {
        if (isEnable)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Vector3 direction = controller.GetPointerLocation() - transform.position;
                Shoot(direction, bulletSpeed);
            }
        }
    }

    private void Shoot(Vector3 direction, float speed)
    {
        GameObject ne = Instantiate(bulletPrefab.gameObject, transform.position, Quaternion.identity);

        ZumaBullet bullet = ne.GetComponent<ZumaBullet>();
        bullet.LaunchBullet(direction, speed);
        bullet.SetOrbData(orbGenerator.GetRandomOrbData());
    }
}
