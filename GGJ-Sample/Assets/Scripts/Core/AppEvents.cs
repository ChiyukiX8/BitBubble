using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppEvents : MonoBehaviour
{
    public static Action<IState> OnGameStateUpdate;
    public static Action<CoinData> OnCoinUpdate;

    public static Action<float> OnBubblePop;

    
}
