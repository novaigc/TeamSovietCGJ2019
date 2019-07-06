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
        fadespeed = 1.0f;
    }

    // Update is called once per frame
    void Update()
    {
        pressanykey();
    }
    void pressanykey()
    {
        CanvasGroup canvasGroup = presskey.GetComponent<CanvasGroup>();
        if (keyisfade==false)
        {
            canvasGroup.alpha -= fadespeed * Time.deltaTime;
            if (canvasGroup.alpha <= 0.25)
                keyisfade = false;
        }
        else
        {
            canvasGroup.alpha += fadespeed * Time.deltaTime;
            if (canvasGroup.alpha >= 0.9)
                keyisfade = true;
        }
    }
}
