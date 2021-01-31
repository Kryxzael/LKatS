using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayOnTrigger : MonoBehaviour
{
    public AudioSource AS;
    public void PlaySound()
	{
		AS.Play();
	}
}
