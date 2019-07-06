using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    public float moveSpeed = 1f;
    public float rotateSpeed = 1f;
    public Animator animator;
    private Vector3 direction;
    private Vector3 moveDirection;
    public SpriteRenderer mySpriteRenderer;
    public bool isCutting = false;
    public enum Direction
    {
        right,
        left
    }
    public Direction facingDirect = Direction.right;
    // Start is called before the first frame update
    void Start()
    {
        direction = Vector3.forward;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isCutting)
        {
            Move2();
        }
        
        //Rotate();
    }

    public void Move()
    {
        Vector3 anyKeyDown = new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), 0);
        transform.position += transform.up * Time.deltaTime * moveSpeed * anyKeyDown.magnitude;
    }



    public void Rotate()
    {
        //float roll = Input.GetAxis("Horizontal") * Time.deltaTime * rotateSpeed;
        //transform.Rotate(0 , 0 , -roll);     
        if (Input.GetAxisRaw("Horizontal") > 0 && Input.GetAxisRaw("Vertical") > 0)
        {
            transform.localRotation = Quaternion.Slerp(transform.localRotation, Quaternion.Euler(0, 0, -45), rotateSpeed * Time.deltaTime);
        }
        if (Input.GetAxisRaw("Horizontal") > 0 && Input.GetAxisRaw("Vertical") < 0)
        {
            transform.localRotation = Quaternion.Slerp(transform.localRotation, Quaternion.Euler(0, 0, -135), rotateSpeed * Time.deltaTime);
        }
        if (Input.GetAxisRaw("Horizontal") < 0 && Input.GetAxisRaw("Vertical") > 0)
        {
            transform.localRotation = Quaternion.Slerp(transform.localRotation, Quaternion.Euler(0, 0, 45), rotateSpeed * Time.deltaTime);
        }
        if (Input.GetAxisRaw("Horizontal") < 0 && Input.GetAxisRaw("Vertical") < 0)
        {
            transform.localRotation = Quaternion.Slerp(transform.localRotation, Quaternion.Euler(0, 0, 135), rotateSpeed * Time.deltaTime);
        }
        if (Input.GetAxisRaw("Horizontal") == 0 && Input.GetAxisRaw("Vertical") > 0)
        {
            transform.localRotation = Quaternion.Slerp(transform.localRotation, Quaternion.Euler(0, 0, 0), rotateSpeed * Time.deltaTime);
        }
        if (Input.GetAxisRaw("Horizontal") == 0 && Input.GetAxisRaw("Vertical") < 0)
        {
            transform.localRotation = Quaternion.Slerp(transform.localRotation, Quaternion.Euler(0, 0, 180), rotateSpeed * Time.deltaTime);
        }
        if (Input.GetAxisRaw("Horizontal") > 0 && Input.GetAxisRaw("Vertical") == 0)
        {
            transform.localRotation = Quaternion.Slerp(transform.localRotation, Quaternion.Euler(0, 0, -90), rotateSpeed * Time.deltaTime);
        }
        if (Input.GetAxisRaw("Horizontal") < 0 && Input.GetAxisRaw("Vertical") == 0)
        {
            transform.localRotation = Quaternion.Slerp(transform.localRotation, Quaternion.Euler(0, 0, 90), rotateSpeed * Time.deltaTime);
        }
        if (Input.GetAxisRaw("Horizontal") == 0 && Input.GetAxisRaw("Vertical") == 0)
        {
            //direction = Vector3.zero;
        }
    }


    public void Move2()
    {
        Vector3 direction = new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), 0);
        transform.position += direction.normalized * moveSpeed * Time.deltaTime;
        if (Input.GetAxisRaw("Horizontal") > 0)
        {
            animator.SetInteger("HorizontalMove", 1);
            facingDirect = Direction.right;
            transform.rotation = Quaternion.Euler(0,0,0);
            //mySpriteRenderer.flipX = false;
        }
        if(Input.GetAxisRaw("Horizontal") < 0)
        {
            animator.SetInteger("HorizontalMove", 1);
            facingDirect = Direction.left;
            transform.rotation = Quaternion.Euler(0, 180, 0);
            //mySpriteRenderer.flipX = true;
        }
        if (Input.GetAxisRaw("Horizontal") == 0)
        {
            animator.SetInteger("HorizontalMove", 0);
        }
        if (Input.GetAxisRaw("Vertical") != 0)
        {
            animator.SetInteger("HorizontalMove", 1);
        }
    }


    private void OnDrawGizmos()
    {
        Gizmos.DrawRay(transform.position, transform.up);
    }
}
