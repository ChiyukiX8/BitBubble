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
        
    }

    // Update is called once per frame
    void Update()
    {
        foreach (CoinData coin in CurrentBubbles)
        {
            coin.UpdateValue();
        }
    }

    public void CreateNewCoin()
    {
        
    }
}
