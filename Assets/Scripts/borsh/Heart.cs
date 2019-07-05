using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heart : MonoBehaviour
{
    private float movespeed;
    private float rotatespeed;
    private float rotatetime = 0;
    float rotateangle = 0;
    // Start is called before the first frame update
    void Start()
    {
        movespeed=0.5f;
        rotatespeed = 4f;
    }

    // Update is called once per frame
    void Update()
    {        
        transform.Translate(Vector3.right * movespeed * Time.deltaTime);
        randomrotate();
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
                transform.Rotate(Vector3.forward, - 2 * angle);
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
}
