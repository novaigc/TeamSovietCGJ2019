using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;
using System.Linq;

public class GameManager { 
    private static GameManager gamemanager;
    public static GameManager gameManager
    {
        get
        {
            if (gamemanager == null)
                gamemanager = new GameManager();
            return gamemanager;
        }
    }
    public float edgeonly = 0.1f;
    public int HP;
    public float movespeed = 1f;
    public bool iseattriangle = false;
    public int[] order = new int[5];
    public string[] name = new string[5] { "AAA", "AAA", "AAA", "AAA", "AAA" };


    public int waves = 0;
    public int score = 0;
    public string curname = "";
    public int baseScore = 55;
    public List<GameObject> hearts = new List<GameObject>();
    public int baseMount = 20;
    public bool isEnd = false;


    public int placeorder(int score)
    {
        int i = 0;
        for (i = 0; i < 5; i++)
            if (score > order[i] || order[i] == 0)
            {
                for (int j = 4; j > i; j--)
                    order[j] = order[j - 1];
                order[i] = score;
                return i;
            }
        return i;
    }
    public void placename(int i)
    {
        for (int j = 4; j > i; j--)
        {
            name[j] = name[j - 1];           
        }
        name[i] = curname;
    }
    public void saveorder()
    {
        for (int i = 0; i < 5; i++)
            PlayerPrefs.SetInt("order" + i.ToString(), order[i]);
            
    }
    public void savename()
    {
        for (int i = 0; i < 5; i++)
            PlayerPrefs.SetString("name" + i.ToString(), name[i]);

    }
    public void loadorder()
    {
        for (int i = 0; i < 5; i++)
        {
            order[i] = PlayerPrefs.GetInt("order" + i.ToString());
        }
    }
    public void loadname()
    {
        for (int i = 0; i < 5; i++)
        {
            name[i] = PlayerPrefs.GetString("name" + i.ToString());
        }
    }
    public void zeroorder()
    {
        for (int i = 0; i < 5; i++)
        {
            PlayerPrefs.SetInt("order" + i.ToString(), 0);
            PlayerPrefs.SetString("name" + i.ToString(), "AAA");
        }
    }
    public AsyncOperation loadscene(string name)
    {
        AsyncOperation op = SceneManager.LoadSceneAsync(name);
        op.allowSceneActivation = false;
        return op;
    }
    public void skip(GameObject image,string scenename)
    {
        AsyncOperation op = loadscene(scenename);
        Tweener tweener= image.GetComponent<Image>().DOFade(1, 1.5f);
        tweener.OnComplete(delegate { op.allowSceneActivation = true; });
    }
    public void addscore()
    {
        score += 10;
    }
    public void endgame(int i)
    {
        //loadorder();
        //placeorder(score);
        if (i < 5)
        {
            loadname();
            placename(i);
            savename();
            saveorder();
        }
        //Debug.Log(order[0]);
        waves = 0;
        movespeed = 1f;
        iseattriangle = false;
        baseMount = 20;
        isEnd = true;
        SceneChanger.Instance.FadeToNextScene();
    }
}
