using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayTest : MonoBehaviour
{ 

    [SerializeField]
    private AudioClip[] audioClips;

    private AudioSource audioSource;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Left Hand" || other.tag == "Right Hand")
        {
            audioSource.clip = audioClips[0];
            audioSource.Play();
        }
    }

}
