using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCController : MonoBehaviour
{
    public bool bReverseNPC;
    public int SpawnIndex;
    public CarMoveSystem _carMoveSystem;
    public CarMoveSystem _playerMoveSystem;
    public void Start()
    {
        _playerMoveSystem = GameManager.Instance.PlayerCar.GetComponent<CarMoveSystem>();
        _carMoveSystem = GetComponent<CarMoveSystem>();
        _carMoveSystem.bReverse = bReverseNPC;

        SetTarget();

        SetPos();

        SetRot();
    }

    void SetTarget()
    {
        if (bReverseNPC == false)
        {
            _carMoveSystem.MaxMotor *= 1.5f;
            SpawnIndex = _carMoveSystem.OutIndex(_playerMoveSystem.TargetPointIndex - 2);
        }
        else SpawnIndex = _carMoveSystem.OutIndex(_playerMoveSystem.TargetPointIndex + 3);
    }

    void SetPos()
    {
        transform.position = GameManager.Instance.WayPoints.GetChild(SpawnIndex).position + Vector3.up;
    }

    void SetRot()
    {
        _carMoveSystem.TargetPointIndex = _carMoveSystem.OutIndex(SpawnIndex + (bReverseNPC ? -1 : 1));
        transform.LookAt(GameManager.Instance.WayPoints.GetChild(_carMoveSystem.TargetPointIndex));
    }

    private void FixedUpdate()
    {
        MoveAI();
    }
    void MoveAI()
    {
        Vector3 WayPointDistance;

        if (Vector3.Distance(transform.position,GameManager.Instance.PlayerCar.transform.position) < 20)
        {
            WayPointDistance = transform.InverseTransformPoint(_playerMoveSystem.transform.position);
            Destroy(gameObject, 10f);
        }
        else
            WayPointDistance = transform.InverseTransformPoint(_carMoveSystem.WayPointList[_carMoveSystem.TargetPointIndex]);

        WayPointDistance.Normalize();
        float steer = WayPointDistance.x;

        _carMoveSystem.MoveWheel(1, steer);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            _carMoveSystem.MaxMotor = 0;
            GetComponent<Rigidbody>().mass = GameInstance.Instance.PartsLevels[2] == 0 ? 400 : 30;
            Destroy(gameObject, 3f);
        }

        if (collision.gameObject.CompareTag("Enemy"))
        {
            GetComponent<Rigidbody>().mass = 20;
            Destroy(gameObject, 3f);
        }
    }
}
