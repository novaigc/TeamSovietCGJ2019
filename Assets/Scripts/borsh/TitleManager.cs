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
    private AudioSource[] audioSource;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponents<AudioSource>();
        audioSource[0].clip = Resources.Load(@"Audios\TITLE") as AudioClip;
        audioSource[0].loop = true;
        audioSource[0].Play();
        //Debug.Log(audioSource[0].clip);
        presskey = GameObject.Find("press any key");
        keyisfade = false;
        fadespeed = 0.5f;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKeyDown)
        {
            start();
        }
        //GameStart();
        //pressanykey();
        //if(!audioSource[0].isPlaying&&isplayed==false)
        //{
        //    audioSource[0].clip= Resources.Load(@"Audios\TITLE2") as AudioClip;
        //    audioSource[0].loop = true;
        //    audioSource[0].Play();
        //    isplayed = true;
        //}
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
       // Debug.Log("dsf");
        StartCoroutine(startgame());
    }
    IEnumerator startgame()
    {
        audioSource[1].clip = Resources.Load(@"Audios\SFX\Select") as AudioClip;
        audioSource[1].loop = false;
        audioSource[1].Play();
        //Debug.Log(audioSource[1].clip);
        yield return new WaitForSeconds(0.4f);
        SceneChanger.Instance.FadeToNextScene();
        GameManager.gameManager.isEnd = false;
    }
}
