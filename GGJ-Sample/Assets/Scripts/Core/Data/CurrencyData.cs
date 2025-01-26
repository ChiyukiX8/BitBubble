using System;
using System.Collections;
using System.Collections.Generic;
using UnityTimer;

public class CoinData
{
    public Guid Id;
    private string _name;
    public string Name {get {return _name;} set {_name = value;}}

    private float _value = 0.0001f;
    public float Value {get {return _value;} set {_value = value;}}

    public float _initialInvestment = 0.0f;
    public float Investment {get  {return _initialInvestment;} set {_initialInvestment = value;}}

    private float _rateOfChange = 1.0f;
    public float Rate {get {return _rateOfChange;}}

    private Timer _timer;

    public List<GrowthBubbleUpgrade> Upgrades = new List<GrowthBubbleUpgrade>();

    public CoinData(BubbleCreationConfig config)
    {
        Id = config.Id;
        Name = config.Name;
        Value = config.InitialValue;

        StartTimer();
    }

    public CoinData(CoinData copy)
    {
        Id = copy.Id;
        _name = copy._name;
        _value = copy._value;
        _initialInvestment = copy._initialInvestment;
        _rateOfChange = copy._rateOfChange;
        
        StartTimer();
    }

    private void StartTimer()
    {
        _timer = Timer.Register(1/_rateOfChange, UpdateValue, isLooped:true);
    }

    private void UpdateTimer()
    {
        _timer.Cancel();
        //_timer = null;
        StartTimer();
    }

    public void UpdateValue()
    {
        _value += 1.0f;
        AppEvents.OnCoinUpdate.Trigger(this);
    }

    public void AddUpgrade(GrowthBubbleUpgrade newUpgrade)
    {
        Upgrades.Add(newUpgrade);
        _rateOfChange = UpgradeSum();
        UpdateTimer();
    }

    public void RemoveUpgrade()
    {

    }

    public float UpgradeSum()
    {
        float sum = 0;
        foreach (GrowthBubbleUpgrade upgrade in Upgrades)
        {
            sum += upgrade.GrowthMagnitude;
        }
        return sum;
    }

    public override string ToString() => $"(Name: {_name}, Value: {_value}, Investment: {_initialInvestment}, Rate: {_rateOfChange})";
}
