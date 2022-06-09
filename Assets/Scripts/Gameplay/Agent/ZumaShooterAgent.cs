using UnityEngine;
using System.Collections;

public class ZumaShooterAgent : ZumaShooter
{
    [SerializeField] private bool enableAutoShoot = true;

    [SerializeField] private ZumaControllerAgent controllerAgent;

    [SerializeField] private float cooldown = 1.0f;
    [SerializeField] private float lastShootCooldown = 1.5f;
    [SerializeField] private int beenSwappedTimes = 2;

    private float cooldownProcess = -1;
    private float lastShootCooldownProcess = -1;
    private int beenSwappedTimesProcess = 0;

    protected override bool CheckShootingTrigger()
    {
        Transform pointer = controllerAgent.GetPointer();
        Vector3 currentPointerPosition = controllerAgent.GetCurrentPointerPos();

        if (cooldownProcess == -1) cooldownProcess = cooldown;

        if (Input.GetMouseButtonDown(0))
        {
            // return true;
        }

        if (enableAutoShoot == false)
        {
            return false;
        }

        if (cooldownProcess > 0)
        {
            cooldownProcess -= Time.deltaTime;
            return false;
        }

        if (beenSwappedTimesProcess >= beenSwappedTimes && lastShootCooldownProcess < 0 // if been swapped orb through maximum times
            || (beenSwappedTimesProcess >= 1 && GetToBeShootedOrbData() == GetToBeShootedNextOrbData()) // or current orb and next orb are the same color (since swapping orb does't give any benefit)
        ) {
            lastShootCooldownProcess = lastShootCooldown;
            beenSwappedTimesProcess = 0;
            return true;
        }

        bool checkedDistance = Vector3.Distance(pointer.position, currentPointerPosition) < 0.32f;

        // If distance of target is minimum
        if (checkedDistance)
        {
            // Debug.Log("Target angle reached");
            // Send sphere cast into the pointer target
            Vector3 direction = (pointer.position - transform.position).normalized;
            LayerMask mask = LayerMask.GetMask("Orb");
            if (Physics.SphereCast(transform.position, 1.5f, direction, out RaycastHit hitInfo, 100, mask)) {
                // Debug.Log("Sphere cast hit");
                Orb hittedOrb = hitInfo.collider.GetComponent<Orb>();
                if (hittedOrb && GetToBeShootedOrbData())
                {
                    // Debug.Log("Hit Orb");
                    // Debug.Log("Color: " + hittedOrb.GetOrbData().GetOrbType() + " vs Color: " + GetToBeShootedOrbData().GetOrbType());
                    // Check if the place to be shooted is the same color
                    if (hittedOrb.GetOrbData().GetOrbType() == GetToBeShootedOrbData().GetOrbType())
                    {
                        Debug.Log("should sHoot");
                        cooldownProcess = cooldown;
                        beenSwappedTimesProcess = 0;
                        return true;
                    }
                }
            }
        }

        return false;
    }

    protected override bool CheckSwapOrbTrigger()
    {
        if (lastShootCooldownProcess == -1) lastShootCooldownProcess = lastShootCooldown;

        if (Input.GetMouseButtonDown(1))
        {
            // return true;
        }

        if (lastShootCooldownProcess > 0)
        {
            lastShootCooldownProcess -= Time.deltaTime;
        }

        if (beenSwappedTimesProcess > beenSwappedTimes)
        {
            return false;
        }

        if (lastShootCooldownProcess <= 0)
        {
            beenSwappedTimesProcess += 1;
            lastShootCooldownProcess = lastShootCooldown;
            return true;
        }

        return false;
    }
}
