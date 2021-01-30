using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float walkSpeed = 2.0f;
    public float maxStamina = 10.0f;
    public float stamina = 5.0f;
    public StaminaBar staminaBar;
    public bool staminaRegeneration;

    private float xSpeed;
    private float zSpeed;
    private Rigidbody body;
    void Start()
    {
        body = this.GetComponent<Rigidbody>();
        staminaBar.setMaxStamina(maxStamina);
    }

    // Update is called once per frame
    void Update()
    {
        xSpeed = 0;
        zSpeed = 0;

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

        xSpeed *= walkSpeed;
        zSpeed *= walkSpeed;

        if (Input.GetKey(KeyCode.LeftShift) && stamina > 0 && (xSpeed != 0 || zSpeed != 0))
        {
            xSpeed *= 2;
            zSpeed *= 2;
            stamina -= Time.deltaTime;
        }
        else if (staminaRegeneration) 
        {
            stamina += Time.deltaTime / 2;
        }

        body.velocity = new Vector3(xSpeed, body.velocity.y, zSpeed);

        stamina = Mathf.Clamp(stamina, 0, maxStamina);
        staminaBar.setStamina(stamina);
    }

    public void gainStamina(float seconds) 
    {
        stamina += seconds;
    }
}
