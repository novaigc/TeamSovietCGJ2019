using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{

    private float timeBtwAttack;
    public float startTimeBtwAttack;

    public Animator animator;
    public Transform attackPos;
    public float attackRange;
    private MovementController movement;
    private Collider2D[] thingsToAttack;

    private void Start()
    {
        movement = gameObject.GetComponent<MovementController>();
    }

    void Update()
    {
        thingsToAttack = Physics2D.OverlapCircleAll(attackPos.position, attackRange, 1 << LayerMask.NameToLayer("heart"));
        if (timeBtwAttack <= 0)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                
                StartCoroutine(StartCutting());
                timeBtwAttack = startTimeBtwAttack;
                animator.SetTrigger("Cut");
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
                StartCoroutine(distroyheart(thingsToAttack[i]));
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPos.position, attackRange);
    }

    IEnumerator StartCutting()
    {
        movement.isCutting = true;
        yield return new WaitForSeconds(0.5f);
        movement.isCutting = false;
    }
    IEnumerator distroyheart(Collider2D heart)
    {
        CameraController.Instance.CamShake();
        heart.transform.GetComponent<Heart>().go.transform.GetComponent<Animator>().SetTrigger("StartBreak");
        heart.transform.GetComponent<CircleCollider2D>().enabled = false;
        heart.transform.GetComponent<Heart>().go.transform.GetComponent<CircleCollider2D>().enabled = false;
        yield return new WaitForSeconds(0.5f);
        Destroy(heart.transform.parent.gameObject);
        GameManager.gameManager.score += 100;
    }
}
