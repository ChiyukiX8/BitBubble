using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class BubbleUpgradeMenu : MonoBehaviour
{
    public static BubbleUpgradeMenu Instance;
    public static Guid OpenedBubble => Instance._openedBubble;

    [SerializeField]
    private GameObject _menuContainer;
    [SerializeField]
    private TextMeshProUGUI _bubbleNameText;
    [SerializeField]
    private TextMeshProUGUI _bubbleValueText;
    [SerializeField]
    private TextMeshProUGUI _bubbleGrowthText;
    [SerializeField]
    private Button _collapseButton;
    [SerializeField]
    private ColoredElementThemer _themer;

    // Probably should dynamically create the upgrades but I guess it's fine to do this since its a game jam
    [SerializeField]
    private BubbleUpgradeButton _newsArticleButton;
    [SerializeField]
    private BubbleUpgradeButton _influencerButton;
    [SerializeField]
    private BubbleUpgradeButton _politicalEndorsementButton;
    [SerializeField]
    private Button _popBubbleButton;

    private GrowthBubbleUpgrade _newsArticleUpgrade;
    private GrowthBubbleUpgrade _influencerUpgrade;
    private GrowthBubbleUpgrade _politicalEndorsementUpgrade;

    private Guid _openedBubble;

    private const string NEWS_ARTICLE_NAME = "News Article";
    private const string NEWS_ARTICLE_DESCRIPTION = "Pay a news outlet to run an article about your currency.";
    private const string INFLUENCER_NAME = "Hire Influencer";
    private const string INFLUENCER_DESCRIPTION = "Pay an influencer to run a social media campaign for your currency.";
    private const string POLITICAL_NAME = "Political Endorsement";
    private const string POLITICAL_DESCRIPTION = "Pay a politician to publicly endourse your currency.";
    private const string POP_DESCRIPTION = "Popping this will take it off the market, and give you all the money others have invested.\nYour trust will decrease for every active buyer.";
    private const string GROWTH_PREFIX = "Growth: ";
    private const string VALUE_PREFIX = "Value: ";

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        _collapseButton.onClick.AddListener(OnCollapseButtonClicked);

        AppEvents.OnCoinUpdate.OnTrigger += OnCoinUpdated;

        InitializeUpgradeValues();

        _newsArticleButton.SetUpgrade(_newsArticleUpgrade);
        _influencerButton.SetUpgrade(_influencerUpgrade);
        _politicalEndorsementButton.SetUpgrade(_politicalEndorsementUpgrade);
        _popBubbleButton.onClick.AddListener(OnPopBubbleClicked);
    }

    private void OnDestroy()
    {
        AppEvents.OnCoinUpdate.OnTrigger -= OnCoinUpdated;
    }

    public void OpenBubbleMenu(Guid bubbleGuid)
    {
        _menuContainer.SetActive(true);

        _openedBubble = bubbleGuid;

        // find bubble creation config via guid and generate menu data
        BubbleCreationConfig config = CurrencyManager.Instance.BubbleConfigLookup(bubbleGuid);
        _themer.SetColorTheme(config.Color);
        _bubbleNameText.text = config.Name;
        // 1. set menu color
        // 2. set name text

        // find coin data via guid and generate menu data
        CoinData coin = CurrencyManager.Instance.CurrentBubbles[_openedBubble];
        // 1. show growth rate
        _bubbleValueText.text = VALUE_PREFIX + Mathf.RoundToInt(coin.Value).ToString();
        // 2. show value
        _bubbleGrowthText.text = GROWTH_PREFIX + coin.Rate.ToString();
    }

    public void Close()
    {
        OnCollapseButtonClicked();
    }

    private void OnCollapseButtonClicked()
    {
        // Make it animate down
        _menuContainer.SetActive(false);
        _openedBubble = Guid.Empty;
    }

    private void InitializeUpgradeValues()
    {
        // Idk what these numbers should be just putting incrementally higher ones for now
        _newsArticleUpgrade = new GrowthBubbleUpgrade(5000, 1.25f, 1.5f, 100, NEWS_ARTICLE_NAME, NEWS_ARTICLE_DESCRIPTION);
        _influencerUpgrade = new GrowthBubbleUpgrade(25000, 2.5f, 2.25f, 200, INFLUENCER_NAME, INFLUENCER_DESCRIPTION);
        _politicalEndorsementUpgrade = new GrowthBubbleUpgrade(100000, 5f, 3.5f, 300, POLITICAL_NAME, POLITICAL_DESCRIPTION);
    }

    private void OnCoinUpdated(CoinData coin)
    {
        if (OpenedBubble.Equals(coin.Id))
        {
            _bubbleGrowthText.text = GROWTH_PREFIX + Mathf.Round(coin.Rate).ToString();
            _bubbleValueText.text = VALUE_PREFIX + coin.Value.ToString();
        }
    }

    private void OnPopBubbleClicked()
    {
        BubbleCreationConfig currentBubble = CurrencyManager.Instance.BubbleConfigLookup(_openedBubble);
        string title = "Pop " + currentBubble.Name + "?";
        string description = POP_DESCRIPTION;
        ConfirmBubbleUpgradeDialog.Instance.DrawPopDialog(title, description);
    }
}
