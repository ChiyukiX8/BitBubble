using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class WealthData
{
    public float TotalValue
    {
        get
        {
            return _totalValue;
        }
        set
        {
            _totalValue = value;
            AppEvents.OnWealthUpdate.Trigger(this);
        }
    }

    public float RealizedGains = 0.0f;

    private float _totalValue = 10000.0f;

    public void UpdateWealth(float addedWealth)
    {
        TotalValue += addedWealth;
    }

    public void UpdateGains()
    {

    }

    public WealthData () {}

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
