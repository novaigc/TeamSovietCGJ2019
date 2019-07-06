using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heart : MonoBehaviour
{
    private float movespeed;
    private float rotatespeed;
    private float rotatetime = 0;
    float rotateangle = 0;
    public GameObject go;
    public Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = transform.GetComponent<Animator>();
        GameManager.gameManager.hearts.Add(this.gameObject);
        //transform.GetComponent<CircleCollider2D>().enabled = false;
        //Invoke("enablecollsion", 1.5f);
        movespeed =GameManager.gameManager.movespeed;
        rotatespeed = 4f;
        go= Instantiate(transform.gameObject, transform.position, Quaternion.identity);
        go.GetComponent<Heart>().enabled = false;
        go.GetComponent<CircleCollider2D>().enabled = false;
        go.GetComponent<SpriteRenderer>().enabled = true;
        //go.transform.parent = this.transform;        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.right * movespeed * Time.deltaTime);      
        if(GameManager.gameManager.waves>=4)
             randomrotate();
        go.transform.position = transform.position;
        if (transform.position.x >= 10 || transform.position.x <= -9 || transform.position.y <= -4 || transform.position.y >= 7)
            Destroy(this.gameObject);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "wall")
        {
            float angle;
            if (collision.transform.name == "rightwall")
            {
                angle = Vector3.Angle(transform.right, collision.transform.up);
                transform.Rotate(Vector3.forward, 2 * angle);
            }
            else if (collision.transform.name == "leftwall")
            {
                angle = Vector3.Angle(transform.right, collision.transform.up);
                transform.Rotate(Vector3.forward, -2 * angle);
            }
            else if (collision.transform.name == "upwall")
            {
                angle = Vector3.Angle(transform.right, collision.transform.right);
                transform.Rotate(Vector3.forward, -2 * angle);
            }
            else if (collision.transform.name == "downwall")
            {
                angle = Vector3.Angle(transform.right, collision.transform.right);
                transform.Rotate(Vector3.forward, 2 * angle);
            }
        }
    }

    private void randomrotate()
    {
        rotatetime += Time.deltaTime;       
        if (rotatetime >= 3)
        {
            rotateangle = Random.Range(-60, 60);
            rotatetime = 0;
        }
        Vector3 rotation = transform.eulerAngles;
        rotation.z += rotateangle;
        transform.eulerAngles = Vector3.Lerp(transform.eulerAngles, rotation, rotatespeed * Time.deltaTime);       
    }

    private void enablecollsion()
    {
        transform.GetComponent<CircleCollider2D>().enabled = true;
    }
    private void OnDestroy()
    {
        GameManager.gameManager.hearts.Remove(this.gameObject);
        Destroy(go);
    }

}
