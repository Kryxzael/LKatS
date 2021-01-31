using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class KillPlayerOnImpact : MonoBehaviour
{
	public bool checkHeight;
	public float height;
	private void OnTriggerEnter(Collider other)
	{
		//Debug.Log(this.transform.position.y);
		if (other.tag== "Player" && ((this.transform.position.y < height && checkHeight)|| !checkHeight))
		{
			UnityEngine.SceneManagement.SceneManager.LoadScene("GameOver");
		}
	}
}
