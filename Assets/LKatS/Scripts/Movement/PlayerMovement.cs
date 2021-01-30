using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float walkSpeed = 2.0f;

    private float speed;
    private float xSpeed;
    private float zSpeed;
    private Rigidbody body;
    void Start()
    {
        body = this.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        xSpeed = 0;
        zSpeed = 0;

        speed = walkSpeed;
        if (Input.GetKey(KeyCode.LeftShift))
        {
            speed *= 2;
        }

        if (Input.GetKey(KeyCode.D))
        {
            xSpeed += transform.right.x;
            zSpeed += transform.right.z;
        }
        if (Input.GetKey(KeyCode.A))
        {
            xSpeed -= transform.right.x;
            zSpeed -= transform.right.z;
        }
        if (Input.GetKey(KeyCode.W))
        {
            xSpeed += transform.forward.x;
            zSpeed += transform.forward.z;
        }
        if (Input.GetKey(KeyCode.S))
        {
            xSpeed -= transform.forward.x;
            zSpeed -= transform.forward.z;
        }

        body.velocity = new Vector3(xSpeed, 0, zSpeed) * speed;
    }
}
