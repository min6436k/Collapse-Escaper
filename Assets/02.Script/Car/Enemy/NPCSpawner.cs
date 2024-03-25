using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class NPCSpawner : MonoBehaviour
{
    public GameObject NPCPrefab;
    public GameObject Danger;

    public List<GameObject> CurrentSpawnNPC = new List<GameObject>();



    public void SpawnNPC()
    {
        Invoke("InvokeSpawn", 0.5f);
    }

    private void InvokeSpawn()
    {
        CurrentSpawnNPC.RemoveAll(x => x == null);

        if (CurrentSpawnNPC.Count >= 3) return;
        NPCController instance = Instantiate(NPCPrefab).GetComponent<NPCController>();

        CurrentSpawnNPC.Add(instance.gameObject);

        switch (Random.Range(0, 2))
        {
            case 0:
                instance.bReverseNPC = false;
                break;
            case 1:
                instance.bReverseNPC = true;
                break;
            case 2:
                break;
        }

        Danger danger = Instantiate(Danger, GameManager.Instance.PlayerCar.transform).GetComponent<Danger>();
        danger.Target = instance.transform;
        Destroy(danger, 6f);
    }
}
