using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppEvents : MonoBehaviour
{
    public static Action<EGameState> OnGameStateUpdate;

    public static Action<BubbleCreationConfig> OnCoinCreation;
    public static Action<CoinData> OnCoinUpdate;

    public static Action<float> OnBubblePop;

    
}
