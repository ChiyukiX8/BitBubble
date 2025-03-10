using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityTimer;

public abstract class BubbleUpgrade
{
    public string Description => _description;
    public string Name => _name;
    public int Cost => _cost;
    public Timer Timer;

    protected int _baseCost;
    protected int _purchaseCount;
    protected float _priceIncreaseMultiplier;
    protected string _description;
    protected string _name;
    protected int _cost;

    public BubbleUpgrade(int baseCost, float priceIncreaseMultiplier, string name, string description)
    {
        _baseCost = baseCost;
        _cost = baseCost;
        _purchaseCount = 0;
        _priceIncreaseMultiplier = priceIncreaseMultiplier;
        _name = name;
        _description = description;
    }
    public bool CanPurchase()
    {
        return CurrencyManager.Instance.Wealth.TotalValue >= _cost;
    }
    public void PurchaseUpgrade(Guid bubbleGuid)
    {
        _cost = Mathf.RoundToInt(_baseCost * (_purchaseCount * _priceIncreaseMultiplier));
        _purchaseCount++;
    }
    public abstract void StartTimer();
    public abstract BubbleUpgrade Copy();
    public abstract void ApplyUpgrade(Guid bubbleGuid);
    public virtual void UpgradeExpired()
    {
        AppEvents.OnUpgradeExpired.Trigger(this);
    }
}

/// <summary>
/// Increases a bubble's growth/rate by magnitude over duration
/// </summary>
public class GrowthBubbleUpgrade : BubbleUpgrade
{
    public float GrowthMagnitude => _growthMagnitude;
    public int Duration => _duration;

    private float _growthMagnitude;
    // In seconds
    private int _duration;

    public GrowthBubbleUpgrade(int baseCost, float priceIncreaseMultiplier, float magnitude, int duration, string name, string description) : base(baseCost, priceIncreaseMultiplier, name, description)
    {
        _growthMagnitude = magnitude;
        _duration = duration;
    }
    public override void StartTimer()
    {
        Timer = Timer.Register(_duration, UpgradeExpired);
    }
    public override void ApplyUpgrade(Guid bubbleGuid)
    {
        // find bubble via guid, apply this upgrade to the coin data

    }
    public override BubbleUpgrade Copy()
    {
        return new GrowthBubbleUpgrade(_baseCost, _priceIncreaseMultiplier, _growthMagnitude, _duration, _name, _description);
    }
}