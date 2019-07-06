using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public bool isActive = false;
    public float delay = 1f;
    public GameObject heart;

    void Start()
    {
        StartCoroutine(Generator());
    }

    IEnumerator Generator()
    {
        yield return new WaitForSeconds(delay);
        if (isActive)
        {
            Instantiate(heart, transform.position , Quaternion.Euler(0, 0, Random.Range(0,180)));
        }
        StartCoroutine(Generator());
    }
}
