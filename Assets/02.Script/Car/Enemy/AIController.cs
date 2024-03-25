using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AIController : MonoBehaviour
{
    private CarMoveSystem _carMoveSystem;
    void Start()
    {
        _carMoveSystem = GetComponent<CarMoveSystem>();
    }

    void FixedUpdate()
    {
        MoveAI();
    }

    void MoveAI()
    {
        Vector3 WayPointDistance = transform.InverseTransformPoint(_carMoveSystem.WayPointList[_carMoveSystem.TargetPointIndex]);
        WayPointDistance.Normalize();
        float steer = WayPointDistance.x;

        _carMoveSystem.MoveWheel(1, steer);
    }
}
