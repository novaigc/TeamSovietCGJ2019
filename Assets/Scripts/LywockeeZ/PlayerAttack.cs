using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{

    private float timeBtwAttack;
    public float startTimeBtwAttack;

    public Animator animator;
    public Transform attackPos;
    public Vector2 attackRange = Vector2.zero;
    private MovementController movement;
    private Collider2D[] thingsToAttack;

    private void Start()
    {
        movement = gameObject.GetComponent<MovementController>();
    }

    void Update()
    {
        thingsToAttack = Physics2D.OverlapCapsuleAll(attackPos.position, attackRange, CapsuleDirection2D.Vertical , 0f , 1 << LayerMask.NameToLayer("heart"));//.OverlapCircleAll(attackPos.position, attackRange, 1 << LayerMask.NameToLayer("heart"));
        if (timeBtwAttack <= 0)
        {
            if (Input.GetKeyDown(KeyCode.J ) && PlayerStats.Instance.isDeath == false)
            {
                
                StartCoroutine(StartCutting());
                timeBtwAttack = startTimeBtwAttack;
                animator.SetTrigger("Cut");
                AudioSource.PlayClipAtPoint(Resources.Load(@"Audios\SFX\Swing") as AudioClip, new Vector3(0, 0, 0));
                Invoke("Destroy", 0.2f);
            }
            
        }
        else
        {
            timeBtwAttack -= Time.deltaTime;
        }
    }

    public void  Destroy()
    {
        //foreach (var item in thingsToAttack)
        //{
        //    if (item != null)
        //    {
        //        Destroy(item.gameObject);
        //        GameManager.gameManager.score += 100;
        //    }
        //}

        for (int i = 0; i < thingsToAttack.Length; i++)
        {
            if (thingsToAttack[i] != null)
            {
                GameObject.Find("Canvasone").GetComponent<CanvasControl>().combineamount++;
                GameObject.Find("Canvasone").GetComponent<CanvasControl>().showcombine();
                StartCoroutine(distroyheart(thingsToAttack[i]));
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(attackPos.position, attackRange);
        //Gizmos.DrawWireSphere(attackPos.position, attackRange);
    }

    IEnumerator StartCutting()
    {
        movement.isCutting = true;
        yield return new WaitForSeconds(0.4f);
        movement.isCutting = false;
    }
    IEnumerator distroyheart(Collider2D heart)
    {
        heart.transform.GetComponent<CircleCollider2D>().enabled = false;
        heart.transform.GetComponent<Heart>().go.transform.GetComponent<CircleCollider2D>().enabled = false;
        CameraController.Instance.CamShake();
        heart.transform.GetComponent<Heart>().go.transform.GetComponent<Animator>().SetTrigger("StartBreak");
        //heart.transform.GetComponent<Heart>().brust.Play();
        AudioSource.PlayClipAtPoint(Resources.Load(@"Audios\SFX\hit") as AudioClip, new Vector3(0, 0, 0));        
        yield return new WaitForSeconds(0.5f);
        //防止报错
        if (heart.transform.parent.gameObject != null)
        {
            Destroy(heart.transform.parent.gameObject);
        } 
        GameManager.gameManager.score += GameManager.gameManager.baseScore;
    }
}
