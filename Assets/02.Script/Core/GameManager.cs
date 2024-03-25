using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    static public GameManager Instance; 

    public GameObject PlayerCar;
    public GameObject EnemyCar;

    public Transform WayPoints;

    public TimeManager TimeManager;

    public GameObject GetItemPrefap;

    public bool bGameEnd = false;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    private void Start()
    {
        GetComponentInChildren<InGameUIManager>().Init(PlayerCar.GetComponent<CarInfo>());
        TimeManager = GetComponentInChildren<TimeManager>();
    }

    public void GameClear()
    {
        bGameEnd = true;
        TimeManager.EndRecord();
        GameInstance.Instance.CurrentScore[GameInstance.Instance.CurrentStage-1] = TimeManager.EndTime;
        GetComponentInChildren<InGameUIManager>().SetClearUI(true);

        if (GameInstance.Instance.CurrentStage == 3)
        {
            GameInstance.Instance.AddRanking();
        }
        else
        {
            GameInstance.Instance.Coin += GameInstance.Instance.CurrentStage * 5000000;
            GameInstance.Instance.CurrentStage++;
        }
    }
    public void Gameover()
    {
        bGameEnd = true;
        GetComponentInChildren<InGameUIManager>().SetClearUI(false);
        TimeManager.EndRecord();
    }

    public void StopTimeTogle()
    {
        GetComponentInChildren<InGameUIManager>().UpdatePartsLevel();

        Time.timeScale = Time.timeScale == 1 ?  0 : 1;
    }

}
