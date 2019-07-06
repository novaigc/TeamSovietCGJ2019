﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndManager : MonoBehaviour
{
    private GameObject[] orders = new GameObject[5];
    // Start is called before the first frame update
    void Start()
    {
        GameManager.gameManager.loadorder();
        for (int i=1;i<=5;i++)
        {
            GameObject.Find("order" + i.ToString()).GetComponent<Text>().text = GameManager.gameManager.order[i - 1].ToString("00000");
        }      
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void retry()
    {
        GameManager.gameManager.score = 0;
        SceneManager.LoadScene("LywockeezTest");
    }
}