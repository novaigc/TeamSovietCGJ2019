using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

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
            PlayerStats.Instance.TakeDamage(0.5f);
            GameManager.gameManager.edgeonly -= 0.1f;
            Destroy(this.gameObject);
            GameObject.Find("Canvas").GetComponent<CanvasControl>().hurtshine();
            Tweener tw= GameObject.Find("Cube").transform.Find("Sprites").GetComponent<SpriteRenderer>().DOFade(0.1f, 0.05f);
            tw.OnComplete(delegate { GameObject.Find("Cube").transform.Find("Sprites").GetComponent<SpriteRenderer>().DOFade(1f, 0.3f); });            
        }
    }
}
