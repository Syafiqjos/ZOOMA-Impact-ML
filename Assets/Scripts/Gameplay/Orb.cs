using UnityEngine;
using System.Collections;

public class Orb : MonoBehaviour
{
    [SerializeField] private SplineFollowerCentered splineFollower;

    public void SetIndex(int index)
    {
        splineFollower.SetOffsetIndex(index);
    }

    public void SetManager(SplineFollowerManager manager)
    {
        splineFollower.SetManager(manager);
    }

    public SplineFollowerCentered GetFollower()
    {
        return splineFollower;
    }
}
