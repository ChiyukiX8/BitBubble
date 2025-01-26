using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioClips : MonoBehaviour
{
    public static AudioClips Instance;

    public AudioClipGroup CreateBubbleSFX;
    public AudioClipGroup PopBubbleSFX;

    public AudioClipGroup BubbleUpgradeNewsSFX;
    public AudioClipGroup BubbleUpgradeInfluencerSFX;
    public AudioClipGroup BubbleUpgradePresidentialSFX;

    public AudioClipGroup SelectConfirmSFX;
    public AudioClipGroup SelectCancelSFX;
    public AudioClipGroup SelectSFX;

    public AudioClipGroupLinear MoneySpendSFX;

    public static AudioClipGroup GetClipGroupByUpgrade(string name)
    {
        switch (name)
        {
            default:
            case Constants.NEWS_ARTICLE_NAME:
                return Instance.BubbleUpgradeNewsSFX;
            case Constants.INFLUENCER_NAME:
                return Instance.BubbleUpgradeInfluencerSFX;
            case Constants.POLITICAL_NAME:
                return Instance.BubbleUpgradePresidentialSFX;
        }
    }
    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
    }
}

[System.Serializable]
public class AudioClipGroup
{
    public List<AudioClip> Clips => _clips;

    [SerializeField]
    protected List<AudioClip> _clips;

    public AudioClip GetRandomClip()
    {
        return _clips[Random.Range(0, _clips.Count)];
    }
}

[System.Serializable]
public class AudioClipGroupLinear : AudioClipGroup
{
    /// <summary>
    /// Returns a clip from the list based on value
    /// </summary>
    public AudioClip GetLinearClip(float value)
    {
        int index = Mathf.RoundToInt(value * (_clips.Count - 1));
        return _clips[index];
    }
}