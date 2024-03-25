using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarInfo : MonoBehaviour
{
    public float SpeedPerHour;
    public int Lap;
    public float[] LabTime = new float[3];

    private Rigidbody rigid;
    private CarMoveSystem _carMoveSystem;
    void Start()
    {
        _carMoveSystem = GetComponent<CarMoveSystem>(); 
        rigid = GetComponent<Rigidbody>();
    }

    void Update()
    {
        SpeedPerHour = rigid.velocity.magnitude * 3.6f;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("FinishLine"))
        {
            Lap++;
            GameManager.Instance.GetComponentInChildren<ItemManager>().SpawnItemInTrack();

            if (_carMoveSystem.bFinishLine == true)
            {
                if (gameObject.CompareTag("Player") && GameManager.Instance.bGameEnd == false)
                {
                    if (Lap > 3) GameManager.Instance.GameClear();
                    else GameManager.Instance.GetComponentInChildren<InGameUIManager>().UpdateLap();

                    if (Lap >= 1) GameManager.Instance.TimeManager.BestTimeCheck();
                }

                if (gameObject.CompareTag("Enemy") && GameManager.Instance.bGameEnd == false)
                {
                    if (Lap > 3) GameManager.Instance.Gameover();
                }
            }

            _carMoveSystem.bFinishLine = false;
        }
    }
}
