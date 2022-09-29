// *---- Purpose of this file: ----*
// Let audio sources listen to data from AudioManager

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ListenToAudioMaster : MonoBehaviour
{
    public float localAudio;

    public AudioManager AM;

    AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        localAudio = audioSource.volume;
    }

    // Update is called once per frame
    void Update()
    {
        audioSource.volume = localAudio * AM.masterVolume;
    }
}
