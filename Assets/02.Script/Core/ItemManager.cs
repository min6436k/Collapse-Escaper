using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BaseItem : MonoBehaviour
{
    public EnumTypes.Item Type;
    public virtual void GetItem()
    {
        GameObject inatence = Instantiate(GameManager.Instance.GetItemPrefap,GameObject.Find("InGameUI").transform);
        GameManager.Instance.PlayerCar.GetComponent<PlayerAudio>().GetItem.Play();

        string name;
        switch (Type)
        {
            case EnumTypes.Item.LowCoin: name = "코인(소)";
                break;
            case EnumTypes.Item.MiddleCoin: name = "코인(중)";
                break;
            case EnumTypes.Item.HighCoin:name = "코인(대)";
                break;
            case EnumTypes.Item.LowBoost:name = "순간 부스트(소)";
                break;
            case EnumTypes.Item.HighBoost:name = "순간 부스트(고)";
                break;
            case EnumTypes.Item.GoShop:name = "상점";
                break;
            default : name = "null";
                break;
        }

        inatence.GetComponent<TextMeshProUGUI>().text = name + "을 획득하였습니다.";
        Destroy(inatence,3);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GetItem();
            Destroy(gameObject);
        }
    }
}

public class ItemManager : MonoBehaviour
{
    public List<GameObject> ItemList = new List<GameObject>();
    public List<GameObject> CoinList = new List<GameObject>();

    public List<GameObject> CurrentSpawnItems = new List<GameObject>();

    public void SpawnItemInTrack()
    {
        foreach (GameObject i in CurrentSpawnItems) Destroy(i);
        CurrentSpawnItems.Clear();


        for(int i = 3; i < GameManager.Instance.WayPoints.childCount; i++)
        {
            if (Random.Range(0, 5) == 0)
            {
                for (int j = -1; j < 2; j++)
                {
                    GameObject instance;

                    int spawnindex = Random.Range(0, ItemList.Count + 1);

                    if (spawnindex == ItemList.Count) instance = Instantiate(CoinList[Random.Range(0, CoinList.Count)], GameManager.Instance.WayPoints.GetChild(i));
                    else instance = Instantiate(ItemList[spawnindex], GameManager.Instance.WayPoints.GetChild(i));

                    instance.transform.localPosition += new Vector3(j * 3f, 0, 0);
                    instance.transform.position = SetGroundPos(instance.transform.position) + new Vector3(0, 1.5f, 0);

                    CurrentSpawnItems.Add(instance);
                }
            }
        }
    }

    private Vector3 SetGroundPos(Vector3 t)
    {
        t += new Vector3(0, 100, 0);
        RaycastHit hit;
        if (Physics.Raycast(t, Vector3.down, out hit, 200,~LayerMask.GetMask("Item")))
        {
            t = hit.point;
        }

        return t;
    }
}
