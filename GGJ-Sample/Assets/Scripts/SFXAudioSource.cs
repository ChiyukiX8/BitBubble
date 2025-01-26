using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXAudioSource : MonoBehaviour
{
    private AudioSource _source;

    public void PlayGlobal(AudioClipGroup clips, float linearValue = -1.0f)
    {
        GlobalAudioSource.PlayAudioClipGroup(clips, linearValue);
    }
    public void PlayLocal(AudioClipGroup clips, float linearValue = -1.0f)
    {
        if (_source == null)
        {
            _source = GetComponent<AudioSource>();
        }

        _source.pitch = GetRandomPitch();

        if (clips is AudioClipGroupLinear linearClips && linearValue != -1.0f)
        {
            _source.PlayOneShot(linearClips.GetLinearClip(linearValue), GetRandomPitch());
        }
        else
        {
            _source.PlayOneShot(clips.GetRandomClip());
        }
    }

    private float GetRandomPitch()
    {
        return Random.Range(0.975f, 1.025f);
    }
}
