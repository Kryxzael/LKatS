using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Mom : MonoBehaviour
{
    public AudioSource Response;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerMovement>() != null)
        {
            SceneManager.LoadScene("Menu");
        }
    }
}
