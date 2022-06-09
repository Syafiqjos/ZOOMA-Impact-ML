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

    private OrbData orbDataToBeShooted;
    private OrbData orbDataToBeShootedNext;

    public delegate void OnShootEventHandler();
    public event OnShootEventHandler OnShoot;

    private void Awake()
    {
        RefreshToBeShootedOrbData();
    }

    private void Update()
    {
        CheckShooting();
    }

    private void CheckShooting()
    {
        if (isEnable)
        {
            if (CheckShootingTrigger())
            {
                Vector3 direction = controller.GetPointerLocation() - transform.position;
                direction.y = 0;

                Shoot(direction.normalized, bulletSpeed);
            }

            if (CheckSwapOrbTrigger()) {
                SwapOrb();
            }
        }
    }

    protected virtual bool CheckShootingTrigger()
    {
        return Input.GetMouseButtonDown(0);
    }

    protected virtual bool CheckSwapOrbTrigger()
    {
        return Input.GetMouseButtonDown(1);
    }

    private void Shoot(Vector3 direction, float speed)
    {
        GameObject ne = Instantiate(bulletPrefab.gameObject, transform.position, Quaternion.identity);

        ZumaBullet bullet = ne.GetComponent<ZumaBullet>();
        bullet.LaunchBullet(direction, speed);
        bullet.SetOrbData(orbDataToBeShooted);

        RefreshToBeShootedOrbData();

        OnShoot?.Invoke();
    }

    private void SwapOrb() {
        OrbData temp = orbDataToBeShootedNext;
        orbDataToBeShootedNext = orbDataToBeShooted;
        orbDataToBeShooted = temp;

        OnShoot?.Invoke();
    }

    public void RefreshToBeShootedOrbData()
    {
        if (orbDataToBeShootedNext) orbDataToBeShooted = orbDataToBeShootedNext;
        else orbDataToBeShooted = orbGenerator.GetRandomOrbData();

        orbDataToBeShootedNext = orbGenerator.GetRandomOrbData();
    }

    public OrbData GetToBeShootedOrbData()
    {
        return orbDataToBeShooted;
    }

    public OrbData GetToBeShootedNextOrbData()
    {
        return orbDataToBeShootedNext;
    }
}
