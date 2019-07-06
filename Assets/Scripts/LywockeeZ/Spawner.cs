using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public bool isActive = false;
    public GameObject[] items;
    public Vector2 angleRange = new Vector2(0,180f);

    void Awake()
    {
        //SpawnerManager.Instance.spawners.Add(this);
    }


    private void Update()
    {
        if (isActive)
        {
            StartCoroutine(Generator());
        }
    }

    IEnumerator Generator()
    {
        Instantiate(items[Random.Range(0,items.Length)], transform.position , Quaternion.Euler(0, 0, Random.Range(angleRange.x,angleRange.y)));
        isActive = false;
        yield return null;
    }

}
