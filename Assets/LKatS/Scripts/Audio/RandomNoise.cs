using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.Mathematics;
using UnityEngine;

public class RandomNoise : MonoBehaviour
{
    public AudioSource audioSource;
    public float volume = 0.5f;
    public float waitTimeMin = 15;
    public float waitTimeMax = 25;
    public float radius = 10;
    public PlayerMovement player;
    public List<AudioClip> clips;

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
            waitTimeLeft = UnityEngine.Random.Range(waitTimeMin, waitTimeMax);
        }

        waitTimeLeft -= Time.deltaTime;

        if (waitTimeLeft <= 0)
        {
            changeLocation();
            audioSource.PlayOneShot(clips[UnityEngine.Random.Range(0, clips.Count-1)], volume);
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
