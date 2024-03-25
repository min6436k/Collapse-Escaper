using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using Unity.VisualScripting;
using UnityEngine;

[Serializable]
public class WheelInfo
{
    public WheelCollider L_Wheel;
    public WheelCollider R_Wheel;

    public bool Motor;
    public bool Steering;
}

public class CarMoveSystem : MonoBehaviour
{
    public WheelInfo[] WheelInfo;

    public float MaxMotor;
    public float MaxSteer;
    public float BreakForce;

    public List<Vector3> WayPointList = new List<Vector3>();
    public float PointArriveDistnace = 15;
    public int TargetPointIndex;

    public bool bReverse = false;

    public bool bFinishLine = false;

    public int MaxTargetPointIndex;
    void Start()
    {
        InitWaySystem();
    }

    void FixedUpdate()
    {
        SearchWay();
    }


    public void MoveWheel(float moterTorque, float steer, bool bIsBreak = false, bool isdrift = false)
    {
        moterTorque *= MaxMotor;
        steer *= MaxSteer;

        foreach (var wheel in WheelInfo)
        {
            if (wheel.Motor)
            {
                wheel.L_Wheel.motorTorque = moterTorque;
                wheel.R_Wheel.motorTorque = moterTorque;
            }

            if (wheel.Steering)
            {
                wheel.L_Wheel.steerAngle = steer;
                wheel.R_Wheel.steerAngle = steer;
            }

            float isbreak = (bIsBreak ? 1 : 0);

            wheel.L_Wheel.brakeTorque = BreakForce * isbreak;
            wheel.R_Wheel.brakeTorque = BreakForce * isbreak;

            SetWheelPos(wheel.L_Wheel);
            SetWheelPos(wheel.R_Wheel);
        }

        WheelFrictionCurve SideF = WheelInfo[1].L_Wheel.sidewaysFriction;

        if (isdrift)
            SideF.stiffness = 2;
        else
            SideF.stiffness = 5;

        WheelInfo[1].L_Wheel.sidewaysFriction = SideF;
        WheelInfo[1].R_Wheel.sidewaysFriction = SideF;
    }

    void InitWaySystem()
    {
        foreach (Transform t in GameManager.Instance.WayPoints) WayPointList.Add(t.position);

        MaxTargetPointIndex = WayPointList.Count - 1;
    }
    void SearchWay()
    {
        TargetPointIndex = OutIndex(TargetPointIndex);

        if (Vector3.Distance(transform.position, WayPointList[TargetPointIndex]) < PointArriveDistnace)
        {
            if(bReverse == false) TargetPointIndex++;
            else TargetPointIndex--;

            if(TargetPointIndex == MaxTargetPointIndex) bFinishLine = true;

            if (gameObject.CompareTag("Player") && UnityEngine.Random.Range(0, 3) == 0)
                GameManager.Instance.GetComponentInChildren<NPCSpawner>().SpawnNPC();
        }

        TargetPointIndex = OutIndex(TargetPointIndex);
    }

    public int OutIndex(int Input)
    {
        if (0 > Input) return MaxTargetPointIndex + Input + 1;
        if (MaxTargetPointIndex < Input)
        {
            return Input - MaxTargetPointIndex - 1;
        }
        return Input;
    }

    void SetWheelPos(WheelCollider wheel)
    {
        Transform Tier = wheel.transform.GetChild(0);
        wheel.GetWorldPose(out Vector3 pos, out Quaternion rot);

        Tier.position = pos;
        Tier.rotation = rot;
    }

}
