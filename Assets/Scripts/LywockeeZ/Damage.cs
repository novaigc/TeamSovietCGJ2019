using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour
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
            PlayerStats.Instance.brust.Play();
            PlayerStats.Instance.TakeDamage(1f);
            GameManager.gameManager.edgeonly -= 0.1f;
            Destroy(this.gameObject);
            GameObject.Find("Canvas").GetComponent<CanvasControl>().hurtshine();
        }
    }
}
