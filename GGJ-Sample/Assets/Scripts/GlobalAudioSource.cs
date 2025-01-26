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

    public static void PlayAudioClipGroup(AudioClipGroup clips, float linearValue = 1.0f)
    {
        if (clips is AudioClipGroupLinear linearClips && linearValue != -1.0f)
        {
            PlayOneShot(linearClips.GetLinearClip(linearValue), GetRandomPitch());
        }
        else
        {
            PlayOneShot(clips.GetRandomClip(), GetRandomPitch());
        }
    }

    public static float GetRandomPitch()
    {
        return Random.Range(0.975f, 1.025f);
    }
}
