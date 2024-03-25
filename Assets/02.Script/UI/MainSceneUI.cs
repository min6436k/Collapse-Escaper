using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainSceneUI : MonoBehaviour
{
    public Stack<GameObject> CurrenrtOpenUI = new Stack<GameObject>();

    public GameObject TitleUI;

    public TextMeshProUGUI Rankings;

    public List<Button> GoStageButton = new List<Button>();

    private void Start()
    {
        CurrenrtOpenUI.Push(TitleUI);

        string[] temp = { "1st : ", "2nd : ", "3rd : ", "4th : ", "5th : " };

        for(int i = 0; i < GameInstance.Instance.BestScore.Count; i++)
        {
            temp[i] += GameInstance.Instance.BestScore[i];
        }

        for (int i = 0; i < 3; i++)
        {
            if (i == GameInstance.Instance.CurrentStage - 1)
                GoStageButton[i].interactable = true;
        }

        Rankings.text = string.Join("\n", temp);
    }
    public void OpenUI(GameObject obj)
    {
        CurrenrtOpenUI.Peek().SetActive(false);
        CurrenrtOpenUI.Push(obj);

        obj.SetActive(true);
    }

    public void CloseUI()
    {
        CurrenrtOpenUI.Pop().SetActive(false);
        CurrenrtOpenUI.Peek().SetActive(true);
    }

    public void GoStage(int stageNum)
    {
        SceneManager.LoadSceneAsync(stageNum);
    }
}
