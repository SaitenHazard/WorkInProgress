using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour {

    private Rigidbody2D rb;

    private float t = 0.0f;
    private bool moving = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        gameObject.transform.Translate(0.0f, 0.0f, 0.0f);
    }

    void Update()
    {
        Vector2 direction = PlayerInstant.Instance.transform.position - transform.position;
        direction = direction.normalized *1 ;
        rb.velocity = direction;


        //Press the Up arrow key to move the RigidBody upwards
        if (Input.GetKey(KeyCode.UpArrow))
        {
            rb.velocity = new Vector2(0.0f, 2.0f);
            moving = true;
            t = 0.0f;
        }

        //Press the Down arrow key to move the RigidBody downwards
        if (Input.GetKey(KeyCode.DownArrow))
        {
            rb.velocity = new Vector2(0.0f, -1.0f);
            moving = true;
            t = 0.0f;
        }

        if (moving)
        {
            // Record the time spent moving up or down.
            // When this is 1sec then display info
            t = t + Time.deltaTime;
            if (t > 1.0f)
            {
                Debug.Log(gameObject.transform.position.y + " : " + t);
                t = 0.0f;
            }
        }
    }
}
