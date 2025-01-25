using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrencyManager : PersistentMonoSingleton<CurrencyManager>
{
    private GameManager _gameManager;
    public PlayerWealth Wealth = new PlayerWealth(10000.0f);

    

    private Dictionary<string,CoinData> CurrentBubbles = new Dictionary<string, CoinData>();

    // Start is called before the first frame update
    void Start()
    {
        AppEvents.OnCoinCreation.OnTrigger += CreateNewCoin;

        if (_gameManager == null) 
        {
            _gameManager = GameManager.Instance;
            AppEvents.OnGameStateUpdate.Trigger(EGameState.Play);
        }
    }

    void OnDestroy()
    {
        AppEvents.OnCoinCreation.OnTrigger -= CreateNewCoin;
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
        CurrentBubbles.Add(newConfig.Name, new CoinData(newConfig));
        
    }
}
