using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class GlobalAudioSource : MonoBehaviour
{
    public static GlobalAudioSource Instance;
    public static AudioSource AudioSource => Instance._audioSource;

    private AudioSource _audioSource;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        _audioSource = GetComponent<AudioSource>();
    }

    public static void PlayOneShot(AudioClip clip, float pitch)
    {
        Instance._audioSource.pitch = pitch;
        Instance._audioSource.PlayOneShot(clip);
    }
}
