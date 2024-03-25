using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameInstance : MonoBehaviour
{
    static public GameInstance Instance;

    public int[] PartsLevels = new int[3] {0, 0, 0};

    public List<float> CurrentScore = new List<float>() {0,0,0};
    public List<float> BestScore = new List<float>() {0,0,0,0,0};

    public int Coin = 0;

    public int CurrentStage = 1;
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddRanking()
    {
        BestScore.Add(CurrentScore.Sum());

        BestScore.Sort();

        BestScore.Reverse();

        if (BestScore.Count > 5) BestScore.RemoveAt(BestScore.Count - 1);

        InitGame();
    }

    public void InitGame()
    {
        CurrentScore = new List<float>() { 0, 0, 0 };
        PartsLevels = new int[3] { 0, 0, 0 };
        Coin = 0;
        CurrentStage = 1;
    }
}
