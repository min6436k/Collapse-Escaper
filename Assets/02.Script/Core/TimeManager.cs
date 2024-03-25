using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public float StartTime = 0;
    public float EndTime = 0;

    public float BestTime = 0;
    public void StartRecord()
    {
        StartTime = Time.time;
    }

    public void EndRecord()
    {
        EndTime = Time.time - StartTime;
    }

    public void BestTimeCheck()
    {
        if(BestTime == 0)
        {
            BestTime = Time.time - StartTime;
        }
        else if(Time.time - BestTime < BestTime)
        {
            BestTime = Time.time - BestTime;
        }
    }
}
