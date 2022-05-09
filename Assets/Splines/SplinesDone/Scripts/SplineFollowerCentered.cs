using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplineFollowerCentered : MonoBehaviour {
    [SerializeField] private SplineFollowerManager manager;

    [SerializeField] private int offsetIndex;
    private float offsetIndexLerp;

    private float moveAmount;
    private float loopAmount;

    private void Update() {
        offsetIndexLerp = Mathf.Lerp(offsetIndexLerp, offsetIndex, 0.1f);

        float moveAmountRaw = manager.GetOffsetInitial() + offsetIndexLerp * manager.GetOffsetInterval() + manager.GetMoveAmount();
        moveAmount = moveAmountRaw % manager.GetMaxMoveAmount();
        loopAmount = moveAmountRaw / manager.GetMaxMoveAmount();

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

    public void SetOffsetIndexForce(int index)
    {
        offsetIndexLerp = index;
        offsetIndex = index;
    }

    public void SetOffsetIndex(int index)
    {
        offsetIndex = index;
    }

    public float GetMoveAmount()
    {
        return moveAmount;
    }

    public float GetLoopAmount()
    {
        return loopAmount;
    }

    public void SetManager(SplineFollowerManager manager)
    {
        this.manager = manager;
    }
}
