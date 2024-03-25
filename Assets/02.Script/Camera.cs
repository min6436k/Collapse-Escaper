using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    CarInfo _carInfo;
    public CinemachineVirtualCamera VirtualCamera;
    void Start()
    {
        _carInfo = GetComponent<CarInfo>();
    }

    void Update()
    {
        VirtualCamera.m_Lens.FieldOfView = Mathf.Lerp(VirtualCamera.m_Lens.FieldOfView, Mathf.Clamp(60 + (_carInfo.SpeedPerHour - 30) * 0.6f, 60, 100),Time.deltaTime);
    }
}
