using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasControl : MonoBehaviour
{
    private GameObject spawner;
    private GameObject score;
    private GameObject waveamount;
    // Start is called before the first frame update
    void Start()
    {
        score = GameObject.Find("score");
        waveamount = GameObject.Find("waveamount");
    }

    // Update is called once per frame
    void Update()
    {
        scoreup();
        showwave();
    }
    public void nextwave()
    {
        if (GameManager.gameManager.hearts.Count == 0)
        {
            GameManager.gameManager.waves++;
            GameManager.gameManager.movespeed += 1;
        }
    }
    public void scoreup()
    {
        score.GetComponent<Text>().text = GameManager.gameManager.score.ToString("00000");
    }
    public void showwave()
    {
        waveamount.GetComponent<Text>().text = "当前第" + GameManager.gameManager.waves.ToString("00") + "波";
    }
}
