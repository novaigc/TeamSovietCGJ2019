using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerManager : MonoBehaviour
{
    public List<Spawner> spawners = new List<Spawner>();
    public float delay = 1f;
    public int heartCount = 0;

    protected static SpawnerManager _instance;
    public static SpawnerManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<SpawnerManager>();
                if (_instance == null)
                {
                    GameObject obj = new GameObject();
                    _instance = obj.AddComponent<SpawnerManager>();
                }
            }
            return _instance;
        }
    }


    void Start()
    {
        StartCoroutine(RandomOpen());
    }


    void Update()
    {
        if (heartCount == GameManager.gameManager.baseMount)
        {
            StopAllCoroutines();
            heartCount = 0;
        }
    }

    IEnumerator RandomOpen()
    {
        yield return new WaitForSeconds(delay);
        spawners[Random.Range(0, spawners.Count - 1)].isActive = true;
        heartCount++;
        StartCoroutine(RandomOpen());
    }

    public void StartGenerate()
    {
        StartCoroutine(RandomOpen());
    }
}
