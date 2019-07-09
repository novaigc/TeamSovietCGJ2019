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
    public int combineamount;
    private GameObject combine;
    private GameObject panel;
    private GameObject input;
    private bool caninput;
    private int ifshow;

    protected static CanvasControl _instance;
    public static CanvasControl Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<CanvasControl>();
                if (_instance == null)
                {
                    GameObject obj = new GameObject();
                    _instance = obj.AddComponent<CanvasControl>();
                }
            }
            return _instance;
        }
    }

    // private GameObject waveamount;
    // Start is called before the first frame update
    void Start()
    {
        ifshow = 5;
        input = GameObject.Find("InputCanvas").transform.Find("Input").gameObject;
        caninput = false;
        panel = transform.Find("Panel").gameObject;
        combineamount = 0;
        combine= transform.Find("combine").gameObject;
        intro = transform.Find("intro").gameObject;
        StartCoroutine(showintro());      
        score = GameObject.Find("score");
        redimage = transform.Find("redimage").gameObject;
        // waveamount = GameObject.Find("waveamount");
        //GameManager.gameManager.zeroorder();
    }

    // Update is called once per frame
    void Update()
    {
        scoreup();
        //showwave();
        if (caninput)
            if (Input.GetKeyDown(KeyCode.Return))
            {
                GameManager.gameManager.curname = input.GetComponent<InputField>().text;
                GameManager.gameManager.endgame(ifshow );
            }
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
        score.GetComponent<Text>().text = GameManager.gameManager.score.ToString();
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
        panel.GetComponent<Image>().DOFade(0, 1f);
        yield return new WaitForSeconds(1f);
        panel.SetActive(false);
    }
    public void showcombine()
    {
        if (combineamount >= 5)
        {
            combine.SetActive(true);
            combine.GetComponent<Text>().text = "连击 " + combineamount.ToString();
            GameManager.gameManager.score += combineamount * 7;
            Tweener tw = combine.GetComponent<RectTransform>().DOBlendableScaleBy(new Vector3(0.5f, 0.5f, 0.5f), 0.1f);
            tw.SetEase(Ease.OutExpo);
            tw.OnComplete(delegate { combine.GetComponent<RectTransform>().DOBlendableScaleBy(new Vector3(-0.5f, -0.5f, -0.5f), 0.2f); });
        }
        else
            combine.SetActive(false);
    }
    public void startinput()
    {
        GameManager.gameManager.loadorder();
        ifshow = GameManager.gameManager.placeorder(GameManager.gameManager.score);
        if (ifshow<5)
        {
            input.SetActive(true);            
            Tweener tw= input.GetComponent<RectTransform>().DOLocalMoveY(36, 2f);
            tw.OnComplete(delegate { caninput = true; });
        }
        else
            GameManager.gameManager.endgame(ifshow);
    }
}
