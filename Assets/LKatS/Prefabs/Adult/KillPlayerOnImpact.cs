using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class KillPlayerOnImpact : MonoBehaviour
{
	private void OnTriggerEnter(Collider other)
	{
		if (other.tag== "Player")
		{
			Debug.Log("Player died");
		}
	}
}
