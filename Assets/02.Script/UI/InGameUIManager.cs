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
                ClearText.text =    $"���ֿ� �ɸ� �ð� : {(int)time / 60}�� {(time % 60).ToString("00.00")}��\n\n+" +
                                    $"��¸� ���� ����� ���÷� ������ �� �ְ� �Ǿ����ϴ�.\n\n" +
                                    $"Ŭ���� ���ھ ��ŷ�� ��ϵǾ����ϴ�.";
            } 
            else
            {
                ClearText.text =    $"���ֿ� �ɸ� �ð� : {(int)time / 60}�� {(time % 60).ToString("00.00")}��\n\n" +
                                    $"������� {GameInstance.Instance.CurrentStage * 500}������ ȹ���Ͽ����ϴ�.\n\n" +
                                    $"���� ���������� ������ �� �ֽ��ϴ�.";
            }
        }
        else
        {
            WinLoseText.text = "2nd,\t\tYou Lose";

            ClearText.text =    $"���̽� ���ֿ��� �й��Ͽ����ϴ�.\n\n" +
                                $"���� ��ȭ�Ͽ� ����ؼ� ������ �� �ֽ��ϴ�.";
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
