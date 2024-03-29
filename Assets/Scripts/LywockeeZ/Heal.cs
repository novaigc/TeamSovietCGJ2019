﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heal : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            AudioSource.PlayClipAtPoint(Resources.Load(@"Audios\SFX\Heal") as AudioClip, new Vector3(0, 0, 0));
            PlayerStats.Instance.Heal(1f);
            Destroy(this.gameObject);
        }
    }
}
