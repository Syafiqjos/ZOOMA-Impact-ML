using UnityEngine;
using System.Collections;

public class SplineFollowerManager : MonoBehaviour
{
    public enum MovementType
    {
        Normalized,
        Units
    }

    [SerializeField] private bool isRunning = true;

    [SerializeField] private SplineDone spline;
    [SerializeField] private float speed = 1f;
    [SerializeField] private MovementType movementType;

    [SerializeField] private float offsetInitial;
    [SerializeField] private float offsetInterval;

    private float maxMoveAmount;
    private float moveAmount;

    private void Start()
    {
        switch (movementType)
        {
            default:
            case MovementType.Normalized:
                maxMoveAmount = 1f;
                break;
            case MovementType.Units:
                maxMoveAmount = spline.GetSplineLength();
                break;
        }
    }

    private void Update()
    {
        if (isRunning)
        {
            moveAmount = (moveAmount + (Time.deltaTime * speed));
        }
    }

    public SplineDone GetSpline() {
        return spline;
    }

    public float GetSpeed()
    {
        return speed;
    }

    public MovementType GetMovementType()
    {
        return movementType;
    }

    public float GetOffsetInitial()
    {
        return offsetInitial;
    }

    public float GetOffsetInterval()
    {
        return offsetInterval;
    }

    public float GetMaxMoveAmount()
    {
        return maxMoveAmount;
    }

    public float GetMoveAmount()
    {
        return moveAmount;
    }

    public void StopMovement() {
        speed = 0;
    }
}
