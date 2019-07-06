using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;

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
    public float edgeonly = 1f;
    public int HP;
    public float movespeed = 1f;
    public bool iseattriangle = false;
    public int[] order = new int[10];
    public int waves = 0;
    public int score = 100;
    public List<GameObject> hearts = new List<GameObject>();
    public int baseMount = 20;
    public bool isEnd = false;

    public void placeorder(int score)
    {
        for (int i = 0; i < 10; i++)
            if (score > order[i] || order[i] == 0)
            {
                for (int j = 9; j > i; j--)
                    order[j] = order[j - 1];
                order[i] = score;
                break;
            }
    }
    public void saveorder()
    {
        for (int i = 0; i < 10; i++)
            PlayerPrefs.SetInt("order" + i.ToString(), order[i]);
    }
    public void loadorder()
    {
        for (int i = 0; i < 10; i++)
        {
            order[i] = PlayerPrefs.GetInt("order" + i.ToString());
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
    public void endgame()
    {
        loadorder();
        placeorder(score);
        saveorder();
        waves = 0;
        movespeed = 1f;
        iseattriangle = false;
        baseMount = 20;
        isEnd = true;
        SceneChanger.Instance.FadeToNextScene();
    }
}
