using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]

public class ZombieAudio : MonoBehaviour
{
    public AudioClip[] footstepSounds;
    public AudioClip[] growlSounds;
    private AudioSource soundSource;
    // Start is called before the first frame update
    void Start()
    {
        soundSource = GetComponent<AudioSource>();
    }

    public void LeftFoot()
    {
        int n = Random.Range(1, footstepSounds.Length);
        soundSource.clip = footstepSounds[n];
        soundSource.PlayOneShot(soundSource.clip);

        footstepSounds[n] = footstepSounds[0];
        footstepSounds[0] = soundSource.clip;
    }
    public void RightFoot()
    {
        int n = Random.Range(1, footstepSounds.Length);
        soundSource.clip = footstepSounds[n];
        soundSource.PlayOneShot(soundSource.clip);

        footstepSounds[n] = footstepSounds[0];
        footstepSounds[0] = soundSource.clip;
    }

    public void GrowlSound()
    {
        int n = Random.Range(1, growlSounds.Length);
        soundSource.clip = growlSounds[n];
        soundSource.PlayOneShot(soundSource.clip);

        growlSounds[n] = growlSounds[0];
        growlSounds[0] = soundSource.clip;
    }


}
