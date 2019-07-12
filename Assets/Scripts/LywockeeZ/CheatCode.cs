using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheatCode : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.KeypadPlus))
        {
            PlayerStats.Instance.Heal(1f);
        }

        if (Input.GetKeyDown(KeyCode.KeypadMinus))
        {
            PlayerStats.Instance.TakeDamage(1f);
        }

        if (Input.GetKeyDown(KeyCode.KeypadMultiply))
        {
            PlayerStats.Instance.AddHealth();
        }
    }
}
