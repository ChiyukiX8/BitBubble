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

    public void DrawDialog(BubbleUpgrade upgrade)
    {
        _menuContainer.SetActive(true);

        _selectedUpgrade = upgrade;

        _upgradeNameText.text = upgrade.Name;
        _descriptionText.text = upgrade.Description;
        string colorTag = upgrade.CanPurchase() ? "<color=#00FF00>" : "<color=#FF0000>";
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

        if(_selectedUpgrade.CanPurchase())
        {
            Debug.Log("UPGRADE PURCHASED");
        }

        _menuContainer.SetActive(false);

    }
    private void OnDialogCancelled()
    {
        _menuContainer.SetActive(false);
    }
}
