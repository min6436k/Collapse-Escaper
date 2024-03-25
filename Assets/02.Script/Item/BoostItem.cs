using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoostItem : BaseItem
{
    public float Time;
    public float Force;
    public override void GetItem()
    {
        base.GetItem();

        GameManager.Instance.PlayerCar.GetComponent<PlayerController>().OnBoost(Time, Force);
        GameManager.Instance.PlayerCar.GetComponentInChildren<PlayerAudio>().PlayBoost();

    }
}
