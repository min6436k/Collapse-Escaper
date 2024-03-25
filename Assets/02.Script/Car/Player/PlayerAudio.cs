using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerAudio : MonoBehaviour
{
    public AudioSource Engine;
    public AudioSource Boost;
    public AudioSource Drift;

    public AudioSource GetItem;


    private Rigidbody _rigid;
    private CarInfo _carInfo;

    private void Start()
    {
        _rigid = GetComponent<Rigidbody>();
        _carInfo = GetComponent<CarInfo>();
    }
    public void PlayBoost()
    {
        Boost.Play();
    }

    public void PlayDrift()
    {
        if (Drift.isPlaying) return;
        Drift.Play();
    }

    public void StopDrift()
    {
        Drift.Stop();
    }

    private void FixedUpdate()
    {
        Engine.volume = Mathf.Clamp((_carInfo.SpeedPerHour - 15)/30, 0, 0.7f);
        Engine.pitch = Mathf.Clamp(1+(_carInfo.SpeedPerHour-30)/100, 1, 1.5f);
    }

}
