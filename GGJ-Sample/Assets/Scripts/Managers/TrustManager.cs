using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrustManager : PersistentMonoSingleton<TrustManager>
{
    public TrustData PlayerTrust;

    // Start is called before the first frame update
    void Start()
    {
        PlayerTrust = new TrustData(1.0f);
        AppEvents.OnTrustUpdate.Trigger(PlayerTrust);
    }

    
}
