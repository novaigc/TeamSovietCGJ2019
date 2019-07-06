using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutJudge : MonoBehaviour
{
    public float radius = 1f;
    private Collider2D[] colliders;
    private CircleCollider2D myCollider;
    void Start()
    {
        myCollider = GetComponent<CircleCollider2D>();
    }

    void Update()
    {
        
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        colliders = Physics2D.OverlapCircleAll(transform.position, radius, 1 << LayerMask.NameToLayer("heart"));

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Invoke("Destroy", 0.2f);
        }
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        colliders = Physics2D.OverlapCircleAll(transform.position, radius, 1 << LayerMask.NameToLayer("heart"));

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Invoke("Destroy", 0.2f);
        }

    }

    public void Destroy()
    { 
        foreach (var item in colliders)
        {
            if (item != null)
            {
                Destroy(item.gameObject);
            }

        }
         
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
