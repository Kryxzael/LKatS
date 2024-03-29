using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class CallMom : MonoBehaviour
{
    private AudioSource _audio;
    private bool isCalling;


    public List<AudioClip> Clips;
    public List<AudioClip> ResponseClips;
    public float SecondsCooldown = 2f;

    private void Awake()
    {
        _audio = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && !isCalling)
        {
            StartCoroutine(CoCallMom());
        }

        IEnumerator CoCallMom()
        {
            isCalling = true;
            if (Clips?.Any() != true)
                throw new InvalidOperationException("The CallMom script does not have any audio clips");

            _audio.clip = Clips[UnityEngine.Random.Range(0, Clips.Count)];
            _audio.Play();

            //Find all walkers
            foreach (Walker i in FindObjectsOfType<Walker>())
            {
                i.GetMad();
            }

            yield return new WaitForSeconds(SecondsCooldown);
            FindObjectOfType<Mom>().Response.clip = ResponseClips[UnityEngine.Random.Range(0, ResponseClips.Count)];
            FindObjectOfType<Mom>().Response.Play();

            isCalling = false;
        }
    }
}
