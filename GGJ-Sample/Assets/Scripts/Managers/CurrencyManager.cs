using System;
using System.Collections.Generic;
using UnityEngine;

public class CurrencyManager : PersistentMonoSingleton<CurrencyManager>
{
    private GameManager _gameManager;
    public WealthData Wealth = new WealthData(10000.0f);


    private Dictionary<Guid,CoinData> CurrentBubbles = new Dictionary<Guid, CoinData>();

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
    }

    void OnDestroy()
    {
        AppEvents.OnCoinCreation.OnTrigger -= CreateNewCoin;
        AppEvents.OnBubblePop.OnTrigger -= BubblePopped;
    }

    // Update is called once per frame
    void Update()
    {
        foreach (var coin in CurrentBubbles)
        {
            coin.Value.UpdateValue();
        }
    }

    public void CreateNewCoin(BubbleCreationConfig newConfig)
    {
        CoinData coin =  new CoinData(newConfig);
        CurrentBubbles.Add(coin.Id, coin);
        
    }

    public void BubblePopped(Guid coinId)
    {
        Wealth.TotalValue += CurrentBubbles[coinId].Value;
    }
}
