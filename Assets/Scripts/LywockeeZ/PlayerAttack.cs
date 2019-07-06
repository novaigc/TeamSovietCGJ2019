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
        if (timeBtwAttack <= 0)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                StartCoroutine(StartCutting());
                timeBtwAttack = startTimeBtwAttack;
                animator.SetTrigger("Cut");
                thingsToAttack = Physics2D.OverlapCircleAll(attackPos.position, attackRange ,1 << LayerMask.NameToLayer("heart"));
                Invoke("Destroy", 0.2f);
            }
            
        }
        else
        {
            timeBtwAttack -= Time.deltaTime;
        }
    }

    public void Destroy()
    {
        for (int i = 0; i < thingsToAttack.Length; i++)
        {
            if (thingsToAttack[i] != null)
            {
                Destroy(thingsToAttack[i].gameObject);
                GameManager.gameManager.score += 100;
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
}
