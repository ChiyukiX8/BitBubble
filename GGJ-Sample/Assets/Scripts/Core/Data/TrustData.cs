using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrustData
{
    public float TotalValue = 1.0f;

    public void UpdateTrust(float addedTrust)
    {
        TotalValue += addedTrust;
    }

    public TrustData(float value)
    {
        TotalValue = value;
    }

    public TrustData(TrustData copy)
    {
        TotalValue = copy.TotalValue;
    }
}
