using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CandyBar : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerMovement>() is PlayerMovement player && player.stamina < player.maxStamina) 
        {
            player.gainStamina(player.maxStamina);

            Destroy(this.gameObject);
        }
    }
}
