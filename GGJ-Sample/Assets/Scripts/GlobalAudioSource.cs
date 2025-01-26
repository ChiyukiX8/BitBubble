using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class GlobalAudioSource : MonoBehaviour
{
    public static GlobalAudioSource Instance;
    public static AudioSource AudioSource => Instance._audioSource;

    private AudioSource _audioSource;

    private float _baseVolume = 0.25f;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        _audioSource = GetComponent<AudioSource>();
    }

    public static void PlayOneShot(AudioClip clip, float pitch = 1.0f, float volumeModifier = 1.0f)
    {
        Instance._audioSource.volume = Instance._baseVolume * volumeModifier;
        Instance._audioSource.pitch = pitch;
        Instance._audioSource.PlayOneShot(clip);
    }

    public static void PlayAudioClipGroup(AudioClipGroup clips, float volumeModifier = 1.0f, float linearValue = 1.0f)
    {
        if (clips is AudioClipGroupLinear linearClips && linearValue != -1.0f)
        {
            PlayOneShot(linearClips.GetLinearClip(linearValue), GetRandomPitch(), volumeModifier);
        }
        else
        {
            PlayOneShot(clips.GetRandomClip(), GetRandomPitch(), volumeModifier);
        }
    }

    public static float GetRandomPitch()
    {
        return Random.Range(0.975f, 1.025f);
    }
}
