using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public bool isActive = false;
    public float delay = 0f;
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
            Instantiate(heart, transform.position , Quaternion.identity);
        }
    }
}
