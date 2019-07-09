using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class EndManager : MonoBehaviour
{
    private GameObject[] orders = new GameObject[5];
    private GameObject score;
    private GameObject tip;
    private GameObject presskey;
    private bool keyisfade;
    private float fadespeed;
    private bool isplayed = false;
    // Start is called before the first frame update
    void Start()
    {
        //GameManager.gameManager.zeroorder();
        tip = transform.Find("tip").gameObject;
        presskey = GameObject.Find("PRESS RESTART");
        keyisfade = false;
        fadespeed = 1.5f;
        GameManager.gameManager.loadorder();
        GameManager.gameManager.loadname();
        for (int i=1;i<=5;i++)
        {
            GameObject.Find("order" + i.ToString()).GetComponent<Text>().text = GameManager.gameManager.order[i - 1].ToString("000000");     
        }
        for (int i = 1; i <= 5; i++)
        {
            GameObject.Find("name" + i.ToString()).GetComponent<Text>().text = GameManager.gameManager.name[i - 1];
        }
        for (int i = 1; i <= 5; i++)
        {
            if (GameObject.Find("name" + i.ToString()).GetComponent<Text>().text == GameManager.gameManager.curname)
                if (GameObject.Find("order" + i.ToString()).GetComponent<Text>().text == GameManager.gameManager.score.ToString("000000"))
                {
                    GameObject.Find("name" + i.ToString()).GetComponent<Text>().color = Color.yellow;
                    GameObject.Find("order" + i.ToString()).GetComponent<Text>().color = Color.yellow;
                    break;
                }
        }


        score = GameObject.Find("score");
        score.GetComponent<Text>().text = "得分：" + GameManager.gameManager.score.ToString("000000");
        if(GameManager.gameManager.score >= GameManager.gameManager.order[0])
        {
            StartCoroutine(showtip());
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKeyDown)
        {
            retry();
        }

        pressanykey();
    }

    void pressanykey()
    {
        CanvasGroup canvasGroup = presskey.GetComponent<CanvasGroup>();
        if (keyisfade == false)
        {
            canvasGroup.alpha -= fadespeed * Time.deltaTime;
            if (canvasGroup.alpha <= 0.05)
                keyisfade = true;
        }
        else
        {
            canvasGroup.alpha += fadespeed * Time.deltaTime;
            if (canvasGroup.alpha >= 0.9)
                keyisfade = false;
        }
    }

    public void retry()
    {
        GameManager.gameManager.curname = "";
        GameManager.gameManager.score = 0;
        GameManager.gameManager.edgeonly = 0.1f;
        SceneChanger.Instance.FadeToScene(0);
        GameManager.gameManager.isEnd = false;
    }
    IEnumerator showtip()
    {
        yield return new WaitForSeconds(0.5f);
        tip.SetActive(true);
        while (true)
        {
            tip.GetComponent<RectTransform>().DOBlendableScaleBy(new Vector3(-0.5f, -0.5f, -0.5f), 1f);
            yield return new WaitForSeconds(1f);
            tip.GetComponent<RectTransform>().DOBlendableScaleBy(new Vector3(0.5f, 0.5f, 0.5f), 1f);
            yield return new WaitForSeconds(1f);
        }
    }


}
