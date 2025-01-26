using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
public class ConfirmBubbleUpgradeDialog : MonoBehaviour
{
    public static ConfirmBubbleUpgradeDialog Instance;

    [SerializeField]
    private GameObject _backgroundBlocker;
    [SerializeField]
    private TextMeshProUGUI _upgradeNameText;
    [SerializeField]
    private TextMeshProUGUI _descriptionText;
    [SerializeField]
    private Button _confirmButton;
    [SerializeField]
    private Button _cancelButton;
    [SerializeField]
    private ColoredElementThemer _themer;
    [SerializeField]
    private TextMeshProUGUI _priceText;
    [SerializeField]
    private UIMenuAnimationController _animController;

    private BubbleUpgrade _selectedUpgrade;


    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        _confirmButton.onClick.AddListener(OnDialogConfirmed);
        _cancelButton.onClick.AddListener(OnDialogCancelled);
    }

    public void DrawPopDialog(string title, string description)
    {
        _backgroundBlocker.SetActive(true);
        _animController.Show();

        _selectedUpgrade = null;
        _upgradeNameText.text = title;
        _descriptionText.text = description;
        _confirmButton.interactable = true;
        _themer.SetColorTheme(CurrencyManager.Instance.BubbleConfigLookup(BubbleUpgradeMenu.OpenedBubble).Color);
        AppEvents.OnCoinUpdate.OnTrigger += UpdatePriceText;
        UpdatePriceText(null);
    }

    private void UpdatePriceText(CoinData data)
    {
        float profit = CurrencyManager.Instance.CurrentBubbles[BubbleUpgradeMenu.OpenedBubble].Profit;
        string profitString = profit.ToString().Trim('-');
        string prefix = profit < 0.0f ? "-" : "+";
        string color = profit < 0.0f ? "<color=#CC4A4A>" : "<color=#4ACC68>";
        _priceText.text = $"{color}{prefix}${profitString}</color>";
    }

    public void DrawDialog(BubbleUpgrade upgrade)
    {
        _backgroundBlocker.SetActive(true);
        _animController.Show();

        _selectedUpgrade = upgrade;

        _upgradeNameText.text = upgrade.Name;
        _descriptionText.text = upgrade.Description;
        string colorTag = upgrade.CanPurchase() ? "<color=#4ACC68>" : "<color=#CC4A4A>";
        _priceText.text = $"Costs: {colorTag}${upgrade.Cost}</color>";
        _confirmButton.interactable = upgrade.CanPurchase();

        _themer.SetColorTheme(CurrencyManager.Instance.BubbleConfigLookup(BubbleUpgradeMenu.OpenedBubble).Color);

        if(upgrade is GrowthBubbleUpgrade growthUpgrade)
        {
            _descriptionText.text += $"\n\nIncreases currency growth by {growthUpgrade.GrowthMagnitude} for {growthUpgrade.Duration} seconds.";
        }
    }

    private void OnDialogConfirmed()
    {
        // Push the upgrade to the bubble stored via guid
        Guid openedbubble = BubbleUpgradeMenu.OpenedBubble;

        if(_selectedUpgrade == null)
        {
            // Selected upgrade is null, for now only case of this is popping.
            // Add new internal state tracking in future if we need more cases
            BubbleUpgradeMenu.Instance.Close();
            Bubble bubble = CurrencyManager.Instance.BubbleLookup(openedbubble);
            CoinData coinData = CurrencyManager.Instance.CurrentBubbles[openedbubble];
            GameplayCanvas.Instance.InitiateMoneyTransfer(bubble, coinData.Value);
            bubble.Pop();

            GlobalAudioSource.PlayAudioClipGroup(AudioClips.Instance.PopBubbleSFX, 0.5f);
            // Reach out to currency manager and give us money for the pop, and call other popping logic
            AppEvents.OnBubblePop.Trigger(openedbubble);
        }
        else
        {
            if (_selectedUpgrade.CanPurchase())
            {
                GlobalAudioSource.PlayAudioClipGroup(AudioClips.GetClipGroupByUpgrade(_selectedUpgrade.Name));
                CurrencyManager.Instance.PurchaseUpgrade(openedbubble, _selectedUpgrade);
            }
        }

        GlobalAudioSource.PlayAudioClipGroup(AudioClips.Instance.SelectConfirmSFX, Constants.UI_SFX_VOLUME_MODIFER);
        Close();

    }
    private void OnDialogCancelled()
    {
        GlobalAudioSource.PlayAudioClipGroup(AudioClips.Instance.SelectCancelSFX, Constants.UI_SFX_VOLUME_MODIFER);
        Close();
    }

    private void Close()
    {
        _backgroundBlocker.gameObject.SetActive(false);
        _animController.Hide();
        AppEvents.OnCoinUpdate.OnTrigger -= UpdatePriceText;
    }
}
