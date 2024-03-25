using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class Part
{
    public EnumTypes.Part type;

    public int MaxUpgradeLevel = 3;

    [TextArea]
    public List<string> LevelText = new List<string>();
    public TextMeshProUGUI TextObj;
    public int Price;

    public Sprite PartImg;
    public Transform LevelImgsObj;
}
public class ShopManager : MonoBehaviour
{
    public List<Part> Parts = new List<Part> ();
    public TextMeshProUGUI CoinText;
    void Start()
    {
        UpdateShopUI();
    }

    public void UpgradePart(int index)
    {
        Part target = Parts.Find(x => x.type == (EnumTypes.Part)index);
        if (target.Price <= GameInstance.Instance.Coin && GameInstance.Instance.PartsLevels[index] < target.MaxUpgradeLevel)
        {
            GameInstance.Instance.PartsLevels[index]++;
            GameInstance.Instance.Coin -= target.Price;
        }

        UpdateShopUI();
        GameManager.Instance?.PlayerCar.GetComponent<PlayerController>().UpdatePartsLevel();
    }

    public void UpdateShopUI()
    {
        CoinText.text = "Coin : " + GameInstance.Instance.Coin;

        for (int i = 0;i < Parts.Count; i++)
        {
            Transform levelImgObj = Parts[i].LevelImgsObj;

            for (int j = 0; j < Parts[i].MaxUpgradeLevel; j++)
            {
                if (GameInstance.Instance.PartsLevels[i] > j)
                    levelImgObj.GetChild(j).GetComponent<Image>().color = Color.white;
            }

            Parts[i].TextObj.text = Parts[i].LevelText[GameInstance.Instance.PartsLevels[i]];
        }
    }
}
