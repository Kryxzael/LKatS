using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CandyBar : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Player") 
        {
            PlayerMovement player = other.gameObject.GetComponent<PlayerMovement>();
            player.gainStamina(5f);

            Destroy(this.gameObject);
        }
    }
}
