using System;
using UnityEngine;
using UnityTimer;

public class TrustManager : PersistentMonoSingleton<TrustManager>
{
    public TrustData PlayerTrust;

    // Start is called before the first frame update
    void Start()
    {
        PlayerTrust = new TrustData(1.0f);
        AppEvents.OnTrustUpdate.Trigger(PlayerTrust);

        Timer.Register(1.5f, OnTrustUpdated, isLooped:true);
    }

    public void OnBubblePopped(Guid id)
    {
        PlayerTrust.TotalValue -= 1 - 1/CurrencyManager.Instance.CurrentBubbles[id].Rate;
        PlayerTrust.Clamp01();
        AppEvents.OnTrustUpdate.Trigger(PlayerTrust);
    }

    private void OnTrustUpdated()
    {
        PlayerTrust.TotalValue += 0.0075f;
        PlayerTrust.Clamp01();

        AppEvents.OnTrustUpdate.Trigger(PlayerTrust);
    }
}
