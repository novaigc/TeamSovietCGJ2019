using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class CanvasControl : MonoBehaviour
{
    private GameObject redimage;
    private GameObject score;
    private GameObject intro;

   // private GameObject waveamount;
    // Start is called before the first frame update
    void Start()
    {
        intro = transform.Find("intro").gameObject;
        StartCoroutine(showintro());      
        score = GameObject.Find("score");
        redimage = transform.Find("redimage").gameObject;
       // waveamount = GameObject.Find("waveamount");
    }

    // Update is called once per frame
    void Update()
    {
        scoreup();
        //showwave();
    }
    public void nextwave()
    {
        
        if (GameManager.gameManager.hearts.Count == 0)
        {
            Debug.Log("ok");
            GameManager.gameManager.baseMount += 17;
            GameManager.gameManager.waves++;
            GameManager.gameManager.movespeed += 1;
            SpawnerManager.Instance.StartGenerate();
        }
    }
    public void scoreup()
    {
        score.GetComponent<Text>().text = GameManager.gameManager.score.ToString("00000");
    }
   // public void showwave()
   // {
    //    waveamount.GetComponent<Text>().text = "当前第" + GameManager.gameManager.waves.ToString("00") + "波";
   // }
   public void hurtshine()
    {
        Tweener tw = redimage.GetComponent<Image>().DOFade(0.5f, 0.1f);
        tw.OnComplete(delegate { redimage.GetComponent<Image>().DOFade(0f, 0.3f); });
    }
    IEnumerator showintro()
    {
        yield return new WaitForSeconds(2f);
        intro.GetComponent<Image>().DOFade(0, 1f);
    }
}
