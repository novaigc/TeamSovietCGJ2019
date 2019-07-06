using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleManager : MonoBehaviour
{
    private GameObject presskey;
    private bool keyisfade;
    private float fadespeed;
    private bool isplayed = false;
    private AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = Resources.Load(@"Audios\TITLE1") as AudioClip;
        audioSource.loop = false;
        audioSource.Play();
        Debug.Log(audioSource.clip);
        presskey = GameObject.Find("press any key");
        keyisfade = false;
        fadespeed = 0.5f;
    }

    // Update is called once per frame
    void Update()
    {
        GameStart();
        pressanykey();
        if(!audioSource.isPlaying&&isplayed==false)
        {
            audioSource.clip= Resources.Load(@"Audios\TITLE2") as AudioClip;
            audioSource.loop = true;
            audioSource.Play();
            isplayed = true;
        }
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
    public void start()
    {

        SceneChanger.Instance.FadeToNextScene();
        GameManager.gameManager.isEnd = false;
    }
}
