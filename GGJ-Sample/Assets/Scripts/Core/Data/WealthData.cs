using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WealthData
{
    public float TotalValue = 10000.0f;

    public float RealizedGains = 0.0f;

    public void UpdateWealth(float addedWealth)
    {
        TotalValue += addedWealth;
    }

    public void UpdateGains()
    {

    }

    public WealthData(float value)
    {
        TotalValue = value;
    }

    public WealthData(WealthData copy)
    {
        TotalValue = copy.TotalValue;
        RealizedGains = copy.RealizedGains;
    }
}
