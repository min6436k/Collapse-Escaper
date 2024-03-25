using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CountDown : MonoBehaviour
{
    int time = 3;
    public void Count()
    {
        --time;
        GetComponent<TextMeshProUGUI>().text = time.ToString();

        if(time == 0) GameObject.FindGameObjectWithTag("FinishLine").GetComponent<BoxCollider>().isTrigger = true;
    }

    public void GameStart()
    {
        GameManager.Instance.GetComponentInChildren<TimeManager>().StartRecord();
    }
}
