using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class InGameUIManager : MonoBehaviour
{
    private CarInfo _playercarInfo;

    public GameObject ShopUI;
    public GameObject InGameUI;
    public GameObject ClearUI;

    public RectTransform SpeedMeterNiddle;
    public TextMeshProUGUI LapText;
    public TextMeshProUGUI TimeText;

    public TextMeshProUGUI WinLoseText;
    public TextMeshProUGUI ClearText;

    public List<Transform> LevelImgs = new List<Transform>();
    public void Init(CarInfo playercarInfo)
    {
        _playercarInfo = playercarInfo;

        UpdatePartsLevel();
    }

    void FixedUpdate()
    {
        SpeedMeterNiddle.rotation = Quaternion.Euler(new Vector3(0, 0, 217 - (_playercarInfo.SpeedPerHour * 1.4f * 1.2f)));

        if (GameManager.Instance.bGameEnd == false && GameManager.Instance.TimeManager.StartTime == 0) return;

        float time = Time.time - GameManager.Instance.TimeManager.StartTime;
        TimeText.text = "TIME : " + (int)time / 60 + ":" + (time % 60).ToString("00.00");
        time = GameManager.Instance.TimeManager.BestTime;
        TimeText.text += "\nBEST : " + (int)time / 60 + ":" + (time % 60).ToString("00.00");
    }

    public void OpenShopUI()
    {
        InGameUI.SetActive(false);
        ShopUI.SetActive(true);
        GameManager.Instance.StopTimeTogle();
    }

    public void SetClearUI(bool win)
    {
        InGameUI.SetActive(false);

        if (win)
        {
            float time = Time.time - GameManager.Instance.TimeManager.StartTime;

            WinLoseText.text = "1st,\t\tYou Win";

            if (GameInstance.Instance.CurrentStage == 3)
            {
                ClearText.text =    $"완주에 걸린 시간 : {(int)time / 60}분 {(time % 60).ToString("00.00")}초\n\n+" +
                                    $"백승리 군은 희망의 도시로 이주할 수 있게 되었습니다.\n\n" +
                                    $"클리어 스코어가 랭킹에 기록되었습니다.";
            } 
            else
            {
                ClearText.text =    $"완주에 걸린 시간 : {(int)time / 60}분 {(time % 60).ToString("00.00")}초\n\n" +
                                    $"상금으로 {GameInstance.Instance.CurrentStage * 500}만원을 획득하였습니다.\n\n" +
                                    $"다음 스테이지에 출전할 수 있습니다.";
            }
        }
        else
        {
            WinLoseText.text = "2nd,\t\tYou Lose";

            ClearText.text =    $"레이싱 경주에서 패배하였습니다.\n\n" +
                                $"차를 강화하여 계속해서 도전할 수 있습니다.";
        }

        ClearUI.SetActive(true);
    }
    public void GoMain()
    {
        SceneManager.LoadSceneAsync("Main");
    }

    public void UpdateLap()
    {
        LapText.text = (_playercarInfo.Lap).ToString();
    }

    public void UpdatePartsLevel()
    {
        for (int i = 0; i < LevelImgs.Count; i++)
        {
            for (int j = 0; j < LevelImgs[i].childCount; j++)
            {
                if (GameInstance.Instance.PartsLevels[i] > j)
                    LevelImgs[i].GetChild(j).GetComponent<Image>().color = Color.white;
            }
        }
    }
}
