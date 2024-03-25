using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effect : MonoBehaviour
{
    public ParticleSystem[] Boosts;
    public ParticleSystem[] Brakes;
    public ParticleSystem Slow;

    private CarInfo _carInfo;
    private ParticleSystem.EmissionModule _emission;
    void Start()
    {
        _carInfo = GetComponent<CarInfo>();
        UpdatePartsLevel();
    }

    public void UpdatePartsLevel()
    {
        for (int i = 0; i < 4; i++)
        {
            if (i < GameInstance.Instance.PartsLevels[(int)EnumTypes.Part.Engine] * 2)
                Boosts[i].gameObject.SetActive(true);
        }
    }

    private void Update()
    {

        foreach (var boost in Boosts)
        {
            _emission = boost.emission;
            _emission.rateOverTime = Mathf.Clamp(_carInfo.SpeedPerHour * 6f, 0, 300);
        }

        InputKey();

    }

    void InputKey()
    {
        if (Input.GetKeyDown(KeyCode.Space) && _carInfo.SpeedPerHour > 1) //속도가 1보다 크고 브레이크 중일 때 킴
        {
            foreach (var Brake in Brakes) Brake.Play();
        }

        if ((Input.GetKey(KeyCode.Space) && _carInfo.SpeedPerHour < 1) || Input.GetKeyUp(KeyCode.Space)) //속도가 1보다 작고 브레이크 중이거나 브레이크를 중지했을 떄ㅐ 끔
        {
            foreach (var Brake in Brakes) Brake.Stop();
        }

        if (Input.GetKeyDown(KeyCode.LeftShift)) //드리프트 중일 때 킴
        {
            foreach (var Brake in Brakes) Brake.Play();
        }

        if (Input.GetKeyUp(KeyCode.LeftShift)) //드리프트 끝날 때 끔
        {
            foreach (var Brake in Brakes) Brake.Stop();
        }
    }
}
