using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RecordScore : MonoBehaviour
{
    public InputField inputField;
    // Start is called before the first frame update
    void Start()
    {
        inputField = GetComponent<InputField>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddScore(string name)
    {
        //GameManager.gameManager.scores.Add(name, GameManager.gameManager.score);
    }

    public void ToEndScene()
    {
        SceneChanger.Instance.FadeToNextScene();
    }
}
