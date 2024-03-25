using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private CarMoveSystem _carMoveSystem;
    private Rigidbody _rigid;
    private Effect _effect;
    private PlayerAudio _audio;
    void Start()
    {
        _carMoveSystem = GetComponent<CarMoveSystem>();
        _rigid = GetComponent<Rigidbody>();
        _effect = GetComponent<Effect>();
        _audio = GetComponent<PlayerAudio>();

        _carMoveSystem.PointArriveDistnace = 25;

        UpdatePartsLevel();
    }

    void Update()
    {
        if(GameManager.Instance.bGameEnd == false)
            MoveInput();
        else
        {
            _carMoveSystem.MoveWheel(0, 0, true, false);
        }
    }

    float SlowCheck(float motor)
    {
        RaycastHit hit;
        if(Physics.Raycast(transform.position,Vector3.down,out hit))
        {
            if (hit.collider.CompareTag("Slow") && GameInstance.Instance.CurrentStage > GameInstance.Instance.PartsLevels[0])
            {
                if(_effect.Slow.isPlaying == false) _effect.Slow.Play();
                return motor / 2f;
            }
            else
            {
                _effect.Slow.Stop();
                return motor;
            }
        }
        return motor;
    }

    void MoveInput()
    {
        float motorTorque = Input.GetAxis("Vertical");
        float steer = Input.GetAxis("Horizontal");
        bool isbreak = Input.GetKey(KeyCode.Space);
        bool isdrift = Input.GetKey(KeyCode.LeftShift);

        if (isdrift) _audio.PlayDrift();
        else _audio.StopDrift();

        _carMoveSystem.MoveWheel(SlowCheck(motorTorque), steer, isbreak, isdrift);
    }

    public void UpdatePartsLevel()
    {
        switch (GameInstance.Instance.PartsLevels[(int)EnumTypes.Part.Engine])
        {
            case 0: _carMoveSystem.MaxMotor = 500; break;
            case 1: _carMoveSystem.MaxMotor = 650; break;
            case 2: _carMoveSystem.MaxMotor = 800; break;
        }

        GetComponent<Effect>().UpdatePartsLevel();
    }

    public void OnBoost(float time, float force)
    {

        StartCoroutine(BoostCoroutine(time, force));
    }

    IEnumerator BoostCoroutine(float time, float force)
    {
        while (time > 0)
        {
            time -= Time.deltaTime;
            _rigid.AddForce(transform.forward * force);

            yield return null;
        }
    }
}
