using System;
using System.Threading.Tasks;
using UnityEngine;


public class AppEvents : MonoBehaviour
{
    public static readonly AppEvent<EGameState> OnGameStateUpdate = new AppEvent<EGameState>();

    public static readonly AppEvent<BubbleCreationConfig> OnCoinCreation = new AppEvent<BubbleCreationConfig>();
    public static readonly AppEvent<CoinData> OnCoinUpdate = new AppEvent<CoinData>();

    public static readonly AppEvent<float> OnBubblePop = new AppEvent<float>();
}

public abstract class BaseEvent
{
    public abstract Type[] GetParameterTypes();
}

public class AppEvent : BaseEvent
{
    public event Action OnTrigger;

    public virtual void Trigger()
    {
        OnTrigger?.Invoke();
    }
    
    public async void TriggerAfter(float seconds)
    {
        await Task.Delay((int) (seconds * 1000));
        Trigger();
    }
    
    public override Type[] GetParameterTypes() => null;
}

public class AppEvent<T> : BaseEvent
{
    public event Action<T> OnTrigger;

    public virtual void Trigger(T value) => OnTrigger?.Invoke(value);
    
    public override Type[] GetParameterTypes() => new[] {typeof(T)};
}

public class AppEvent<T1, T2> : BaseEvent
{
    public event Action<T1, T2> OnTrigger;

    public virtual void Trigger(T1 value1, T2 value2) => OnTrigger?.Invoke(value1, value2);
    
    public override Type[] GetParameterTypes() => new[] {typeof(T1), typeof(T2)};
}

public class AppEvent<T1, T2, T3> : BaseEvent
{
    public event Action<T1, T2, T3> OnTrigger;

    public virtual void Trigger(T1 value1, T2 value2, T3 value3) => OnTrigger?.Invoke(value1, value2, value3);
    
    public override Type[] GetParameterTypes() => new[] {typeof(T1), typeof(T2), typeof(T3)};
}