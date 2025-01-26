using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class BubbleUpgradeButton : MonoBehaviour
{
    private Button _button;
    private BubbleUpgrade _upgrade;

    private void Awake()
    {
        if(_button == null)
        {
            _button = GetComponent<Button>();
        }
        _button.onClick.AddListener(OnButtonClicked);
    }
    public void SetUpgrade(BubbleUpgrade upgrade)
    {
        _upgrade = upgrade;
    }

    private void OnButtonClicked()
    {
        ConfirmBubbleUpgradeDialog.Instance.DrawDialog(_upgrade);

        GlobalAudioSource.PlayAudioClipGroup(AudioClips.Instance.SelectSFX, Constants.UI_SFX_VOLUME_MODIFER);
    }
}
