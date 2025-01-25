using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrencyManager : PersistentMonoSingleton<CurrencyManager>
{
    public float InitalWealth = 10000.0f;

    public List<CoinData> CurrentBubbles;

    // Start is called before the first frame update
    void Start()
    {
        AppEvents.OnCoinCreation += CreateNewCoin;
    }

    void OnDestroy()
    {
        AppEvents.OnCoinCreation -= CreateNewCoin;
    }

    // Update is called once per frame
    void Update()
    {
        foreach (CoinData coin in CurrentBubbles)
        {
            coin.UpdateValue();
        }
    }

    public void CreateNewCoin(BubbleCreationConfig newConfig)
    {
        CurrentBubbles.Add(new CoinData(newConfig));
    }
}
