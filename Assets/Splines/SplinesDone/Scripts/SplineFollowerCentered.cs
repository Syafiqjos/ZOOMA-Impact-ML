using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplineFollowerCentered : MonoBehaviour {
    [SerializeField] private SplineFollowerManager manager;
    [SerializeField] private int offsetIndex;

    private void Update() {
        float moveAmount = (manager.GetOffsetInitial() + offsetIndex * manager.GetOffsetInterval() + manager.GetMoveAmount()) % manager.GetMaxMoveAmount();

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
}
