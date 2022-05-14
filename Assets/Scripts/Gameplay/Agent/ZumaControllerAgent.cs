﻿using UnityEngine;
using System.Collections;

public class ZumaControllerAgent : ZumaController
{
    [SerializeField] private bool enableTrackOrb = true;
    [SerializeField] private bool enableSmoothTracking = true;

    [SerializeField] private ZumaShooter shooter;

    [SerializeField] private Transform pointer;
    [SerializeField] private OrbGenerator orbGenerator;

    private Vector3 currentPointerPos;

    public override Vector3 GetPointerLocation()
    {
        if (enableTrackOrb)
        {
            TrackOrb();
        }

        if (enableSmoothTracking)
        {
            currentPointerPos = Vector3.Lerp(currentPointerPos, pointer.position, 0.1f);
        } else
        {
            currentPointerPos = pointer.position;
        }


        return currentPointerPos;
    }

    public void MovePointer(Vector3 pos)
    {
        pointer.position = pos;
    }

    public Transform GetPointer()
    {
        return pointer;
    }

    private void TrackOrb()
    {
        // Track by priority
        if (TrackEqualForemostSequentialOrb()) { }
        else if (TrackEqualForemostOrb()) { }
        else if (TrackForemostOrb()) { }
    }

    private bool TrackEqualForemostSequentialOrb()
    {
        Orb sequentialOrb = orbGenerator.GetExistForemostSequentialOrb(shooter.GetToBeShootedOrbData());

        if (sequentialOrb)
        {
            Orb prevOrb = sequentialOrb.GetPrevOrb();
            Vector3 pivotPos = (sequentialOrb.transform.position + prevOrb.transform.position) / 2;

            MovePointer(pivotPos);

            return true;
        }

        return false;
    }

    private bool TrackEqualForemostOrb()
    {
        Orb equalOrb = orbGenerator.GetExistForemostOrb(shooter.GetToBeShootedOrbData());

        if (equalOrb)
        {
            Vector3 pivotPos = equalOrb.transform.position;

            MovePointer(pivotPos);

            return true;
        }

        return false;
    }

    private bool TrackForemostOrb()
    {
        Orb foremostOrb = orbGenerator.GetExistForemostOrb();

        if (foremostOrb)
        {
            Vector3 pivotPos = foremostOrb.transform.position;

            MovePointer(pivotPos);

            return true;
        }

        return false;
    }
}
