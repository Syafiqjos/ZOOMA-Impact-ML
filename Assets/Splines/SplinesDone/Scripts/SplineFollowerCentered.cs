using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplineFollowerCentered : MonoBehaviour {
    [SerializeField] private SplineFollowerManager manager;

    [SerializeField] private int offsetIndex;
    private float offsetIndexLerp;

    private float moveAmount;

    private void Update() {
        offsetIndexLerp = Mathf.Lerp(offsetIndexLerp, offsetIndex, 0.1f);

        moveAmount = (manager.GetOffsetInitial() + offsetIndexLerp * manager.GetOffsetInterval() + manager.GetMoveAmount()) % manager.GetMaxMoveAmount();

        SplineDone spline = manager.GetSpline();

        switch (manager.GetMovementType()) {
            default:
            case SplineFollowerManager.MovementType.Normalized:
                transform.position = spline.GetPositionAt(moveAmount);
                transform.forward = spline.GetForwardAt(moveAmount);
                break;
            case SplineFollowerManager.MovementType.Units:
                transform.position = spline.GetPositionAtUnits(moveAmount);
                transform.forward = spline.GetForwardAtUnits(moveAmount);
                break;
        }
    }

    public int GetOffsetIndex()
    {
        return offsetIndex;
    }

    public void SetOffsetIndex(int index)
    {
        offsetIndex = index;
    }

    public float GetMoveAmount()
    {
        return moveAmount;
    }

    public void SetManager(SplineFollowerManager manager)
    {
        this.manager = manager;
    }
}
