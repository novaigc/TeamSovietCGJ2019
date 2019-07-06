using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerManager : MonoBehaviour
{
    public List<Spawner> spawners = new List<Spawner>();
    public float delay = 1f;
    public int spawnerCount = 0;
    public int gameLevel = 1;
    float increaseSpeed = 0f;
    public float IncreaseSpeed { get { return increaseSpeed; } set { increaseSpeed = value; } }

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
        StartCoroutine(ChangeSpeed());
    }


    void Update()
    {
        ChangeGameLevel();
        //if (heartCount == GameManager.gameManager.baseMount)
        //{
        //    StopAllCoroutines();
        //    heartCount = 0;
        //}
    }

    IEnumerator RandomOpen()
    {
        yield return new WaitForSeconds(delay);
        spawners[Random.Range(0, spawnerCount - 1)].isActive = true;
        //heartCount++;
        StartCoroutine(RandomOpen());
    }

    public void StartGenerate()
    {
        StartCoroutine(RandomOpen());
    }

    IEnumerator ChangeSpeed()
    {
        increaseSpeed += 0.05f;
        Debug.Log(increaseSpeed);
        yield return new WaitForSeconds(1f);
        StartCoroutine(ChangeSpeed());
    }

    public void ChangeGameLevel()
    {
        if (increaseSpeed >= 1 && increaseSpeed < 2)
        {
            gameLevel = 1;
            spawnerCount = 4;
            delay = 1.5f;
            GameManager.gameManager.movespeed = 1.2f;
        }

        if (increaseSpeed >= 2 && increaseSpeed < 3)
        {
            gameLevel = 2;
            spawnerCount = 8;
            delay = 1.5f;
            GameManager.gameManager.movespeed = 2f;
        }

        if (increaseSpeed >= 3 && increaseSpeed < 4.5)
        {
            gameLevel = 3;
            spawnerCount = 12;
            delay = 1f;
            GameManager.gameManager.movespeed = 2.5f;
        }

        if (increaseSpeed >= 4.5)
        {
            gameLevel = 4;
            spawnerCount = 16;
            delay = 0.5f;
            GameManager.gameManager.movespeed = 3f;
        }
    }
}
