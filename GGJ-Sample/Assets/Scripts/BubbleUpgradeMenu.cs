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

    private GrowthBubbleUpgrade _newsArticleUpgrade;
    private GrowthBubbleUpgrade _influencerUpgrade;
    private GrowthBubbleUpgrade _politicalEndorsementUpgrade;

    private Guid _openedBubble;

    private const string NEWS_ARTICLE_NAME = "News Article";
    private const string NEWS_ARTICLE_DESCRIPTION = "Pay a news outlet to run an article about your currency";
    private const string INFLUENCER_NAME = "Hire Influencer";
    private const string INFLUENCER_DESCRIPTION = "Pay an influencer to run a social media campaign for your currency";
    private const string POLITICAL_NAME = "Political Endorsement";
    private const string POLITICAL_DESCRIPTION = "Pay a politician to publicly endourse your currency";

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        _collapseButton.onClick.AddListener(OnCollapseButtonClicked);

        InitializeUpgradeValues();

        _newsArticleButton.SetUpgrade(_newsArticleUpgrade);
        _influencerButton.SetUpgrade(_influencerUpgrade);
        _politicalEndorsementButton.SetUpgrade(_politicalEndorsementUpgrade);
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
        // 1. show growth rate
        // 2. show value
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
}
