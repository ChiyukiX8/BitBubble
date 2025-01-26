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
    private GameObject _menuContainer;
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
        _menuContainer.SetActive(true);

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
        _priceText.text = $"<color=#4ACC68>+${CurrencyManager.Instance.CurrentBubbles[BubbleUpgradeMenu.OpenedBubble].Value}</color>";
    }

    public void DrawDialog(BubbleUpgrade upgrade)
    {
        _menuContainer.SetActive(true);

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
            // idc anymore just use gameobject.find :'(
            CurrencyManager.Instance.BubbleLookup(openedbubble).Pop();

            // Reach out to currency manager and give us money for the pop, and call other popping logic
            AppEvents.OnBubblePop.Trigger(openedbubble);
        }
        else
        {
            if (_selectedUpgrade.CanPurchase())
            {
                Debug.Log("UPGRADE PURCHASED");
                CurrencyManager.Instance.PurchaseUpgrade(openedbubble, _selectedUpgrade);
            }
        }


        _menuContainer.SetActive(false);
        AppEvents.OnCoinUpdate.OnTrigger -= UpdatePriceText;

    }
    private void OnDialogCancelled()
    {
        _menuContainer.SetActive(false);
        AppEvents.OnCoinUpdate.OnTrigger -= UpdatePriceText;
    }
}
