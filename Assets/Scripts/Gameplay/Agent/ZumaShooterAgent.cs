using UnityEngine;
using System.Collections;

public class ZumaShooterAgent : ZumaShooter
{
    [SerializeField] private bool enableAutoShoot = true;

    [SerializeField] private ZumaControllerAgent controllerAgent;

    [SerializeField] private float cooldown = 1.0f;

    private float cooldownProcess = -1;

    protected override bool CheckShootingTrigger()
    {
        Transform pointer = controllerAgent.GetPointer();
        Vector3 currentPointerPosition = controllerAgent.GetCurrentPointerPos();

        if (cooldownProcess == -1) cooldownProcess = cooldown;

        if (Input.GetMouseButtonDown(0))
        {
            return true;
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
                        return true;
                    }
                }
            }
        }

        return false;
    }
}
