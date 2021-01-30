using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float walkSpeed = 2.0f;

    private float speed;
    private Rigidbody body;
    void Start()
    {
        body = this.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        speed = walkSpeed;
        if (Input.GetKey(KeyCode.LeftShift))
        {
            speed *= 2;
        }

        if (Input.GetKey(KeyCode.D))
        {
            body.velocity = transform.right * speed;
        }
        if (Input.GetKey(KeyCode.A))
        {
            body.velocity = -transform.right * speed;
        }
        if (Input.GetKey(KeyCode.W))
        {
            body.velocity = transform.forward * speed;
        }
        if (Input.GetKey(KeyCode.S))
        {
            body.velocity = -transform.forward * speed;
        }

        body.velocity = new Vector3(body.velocity.x, 0, body.velocity.z);
    }
}
