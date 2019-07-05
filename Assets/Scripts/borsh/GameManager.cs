using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    public int HP;
}
