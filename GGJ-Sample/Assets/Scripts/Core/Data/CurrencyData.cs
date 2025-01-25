using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWealth
{
    public float Value = 10000.0f;

    public float RealizedGains = 0.0f;

    public void UpdateWealth(float addedWealth)
    {
        Value += addedWealth;
    }

    public void UpdateGains()
    {

    }
}

public class CoinData
{
    private string _name;
    public string Name {get {return _name;} set {_name = value;}}

    private float _value = 0.0001f;
    public float Value {get {return _value;} set {_value = value;}}

    public float _initialInvestment = 0.0f;
    public float InitialInvestment {get  {return _initialInvestment;} set {_initialInvestment = value;}}

    private float _rateOfChange = 0.01f;
    public float Rate {get {return _rateOfChange;}}

    public void UpdateValue()
    {
        _value *= _rateOfChange;
    }

    public override string ToString() => $"(Value: {_value}, Rate: {_rateOfChange})";
}
