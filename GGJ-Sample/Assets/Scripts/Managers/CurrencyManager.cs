using System;
using System.Collections.Generic;
using UnityEngine;
using UnityTimer;

public class CurrencyManager : PersistentMonoSingleton<CurrencyManager>
{
    private GameManager _gameManager;

    private Timer _coinUpdateTimer;
    public WealthData Wealth = new WealthData(75000.0f);


    public Dictionary<Guid,CoinData> CurrentBubbles = new Dictionary<Guid, CoinData>();

    // Start is called before the first frame update
    void Start()
    {
        AppEvents.OnCoinCreation.OnTrigger += CreateNewCoin;
        AppEvents.OnBubblePop.OnTrigger += BubblePopped;

        if (_gameManager == null) 
        {
            _gameManager = GameManager.Instance;
            AppEvents.OnGameStateUpdate.Trigger(EGameState.Play);
        }

        AppEvents.OnWealthUpdate.Trigger(Wealth);
    }

    void OnDestroy()
    {
        AppEvents.OnCoinCreation.OnTrigger -= CreateNewCoin;
        AppEvents.OnBubblePop.OnTrigger -= BubblePopped;
    }

    public void CreateNewCoin(BubbleCreationConfig newConfig)
    {
        CoinData coin =  new CoinData(newConfig);
        CurrentBubbles.Add(coin.Id, coin);
    }

    public void PurchaseUpgrade(Guid id, BubbleUpgrade upgrade)
    {
        if (upgrade is GrowthBubbleUpgrade)
        {
            Wealth.TotalValue -=  upgrade.Cost;
            CurrentBubbles[id].AddUpgrade((GrowthBubbleUpgrade)upgrade);
        }
    }

    public BubbleCreationConfig BubbleConfigLookup(Guid id)
    {
        var bubbles = GameObject.FindObjectsByType<Bubble>(FindObjectsSortMode.None);
        foreach (Bubble bubble in bubbles)
        {
            if (bubble.config.Id == id)
            {
                return bubble.config;
            }
        }
        // Bubble not found
        return null;
    }

    public Bubble BubbleLookup(Guid id)
    {
        var bubbles = GameObject.FindObjectsByType<Bubble>(FindObjectsSortMode.None);
        foreach (Bubble bubble in bubbles)
        {
            if (bubble.config.Id == id)
            {
                return bubble;
            }
        }
        // Bubble not found
        return null;
    }

    public void BubblePopped(Guid coinId)
    {
        float value = CurrentBubbles[coinId].Value;
        Wealth.TotalValue += value;
        GlobalAudioSource.PlayAudioClipGroup(AudioClips.Instance.MoneyAddSFX, 1.0f, AudioClips.ConvertWealthValueToLinear(Mathf.RoundToInt(value)));
        TrustManager.Instance.OnBubblePopped(coinId);
        CurrentBubbles.Remove(coinId);
    }
}
