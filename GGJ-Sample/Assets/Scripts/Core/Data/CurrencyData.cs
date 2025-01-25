using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinData
{
    public Guid Id;
    private string _name;
    public string Name {get {return _name;} set {_name = value;}}

    private float _value = 0.0001f;
    public float Value {get {return _value;} set {_value = value;}}

    public float _initialInvestment = 0.0f;
    public float Investment {get  {return _initialInvestment;} set {_initialInvestment = value;}}

    private float _rateOfChange = 0.01f;
    public float Rate {get {return _rateOfChange;}}

    public CoinData(BubbleCreationConfig config)
    {
        Id = config.Id;
        Name = config.Name;
        Value = config.InitialValue;
    }

    public CoinData(CoinData copy)
    {
        Id = copy.Id;
        _name = copy._name;
        _value = copy._value;
        _initialInvestment = copy._initialInvestment;
        _rateOfChange = copy._rateOfChange;
    }

    public void UpdateValue()
    {
        _value *= _rateOfChange;
        AppEvents.OnCoinUpdate.Trigger(this);
    }

    public override string ToString() => $"(Name: {_name}, Value: {_value}, Investment: {_initialInvestment}, Rate: {_rateOfChange})";
}
