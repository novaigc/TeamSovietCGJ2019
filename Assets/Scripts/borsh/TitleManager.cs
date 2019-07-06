using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleManager : MonoBehaviour
{
    private GameObject presskey;
    private bool keyisfade;
    private float fadespeed;
    // Start is called before the first frame update
    void Start()
    {
        presskey = GameObject.Find("press any key");
        keyisfade = false;
        fadespeed = 0.5f;
    }

    // Update is called once per frame
    void Update()
    {
        GameStart();
        pressanykey();
    }
    void pressanykey()
    {
        CanvasGroup canvasGroup = presskey.GetComponent<CanvasGroup>();
        if (keyisfade==false)
        {
            canvasGroup.alpha -= fadespeed * Time.deltaTime;
            if (canvasGroup.alpha <= 0.25)
                keyisfade = true;
        }
        else
        {
            canvasGroup.alpha += fadespeed * Time.deltaTime;
            if (canvasGroup.alpha >= 0.9)
                keyisfade = false;
        }
    }

    void GameStart()
    {
        if (Input.anyKeyDown)
        {
            SceneChanger.Instance.FadeToNextScene();
            GameManager.gameManager.isEnd = false;
        }
    }
}
