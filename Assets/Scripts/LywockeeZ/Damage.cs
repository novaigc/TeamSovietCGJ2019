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
            GameObject.Find("Canvasone").GetComponent<CanvasControl>().combineamount=0;
            GameObject.Find("Canvasone").GetComponent<CanvasControl>().showcombine();
            PlayerStats.Instance.brust.Play();
            AudioSource.PlayClipAtPoint(Resources.Load(@"Audios\SFX\heartbeat") as AudioClip, new Vector3(0, 0, 0));
            PlayerStats.Instance.TakeDamage(1f);
            //GameManager.gameManager.edgeonly -= 0.2f;
            Destroy(this.gameObject);
            GameObject.Find("Canvasone").GetComponent<CanvasControl>().hurtshine();
            Tweener tw= GameObject.Find("Cube").transform.Find("Sprites").GetComponent<SpriteRenderer>().DOFade(0.1f, 0.05f);
            tw.OnComplete(delegate { GameObject.Find("Cube").transform.Find("Sprites").GetComponent<SpriteRenderer>().DOFade(1f, 0.3f); });            
        }
    }
}
