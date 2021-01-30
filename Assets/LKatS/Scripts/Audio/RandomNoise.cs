using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.WSA;

public class RandomNoise : MonoBehaviour
{
    public AudioSource audioSource;
    public float volume = 0.5f;
    public float waitTime = 6;
    public float radius = 10;
    public PlayerMovement player;
    public AudioClip clip1;
    public AudioClip clip2;
    public AudioClip clip3;
    public AudioClip clip4;
    public AudioClip clip5;

    private float waitTimeLeft = 0;


    private void Update()
    {
        if (audioSource.isPlaying) 
        {
            waitTimeLeft = 0;
            return;
        }

        if (waitTimeLeft <= 0) 
        {
            waitTimeLeft = UnityEngine.Random.Range(Mathf.Round(waitTime - (waitTime/2)), Mathf.Round(waitTime + (waitTime / 2)));
        }

        waitTimeLeft -= Time.deltaTime;

        if (waitTimeLeft <= 0)
        {
            changeLocation();

            switch (UnityEngine.Random.Range(0, 4)) 
            {
                case 0:
                    audioSource.PlayOneShot(clip1, volume);
                break;
                case 1:
                    audioSource.PlayOneShot(clip2, volume); 
                break;
                case 2:
                    audioSource.PlayOneShot(clip3, volume); 
                break;
                case 3: 
                    audioSource.PlayOneShot(clip4, volume); 
                break;
                case 4: 
                    audioSource.PlayOneShot(clip5, volume); 
                break;
            }
        }
    }

    private void changeLocation()
    {
        Vector3 playerPosition = player.gameObject.transform.position;
        Vector3 randomPoint = UnityEngine.Random.onUnitSphere.normalized;
        Vector3 relativePosition = new Vector3(randomPoint.x * radius, 7, randomPoint.z * radius);
        this.transform.position = playerPosition + relativePosition;
    }
}
