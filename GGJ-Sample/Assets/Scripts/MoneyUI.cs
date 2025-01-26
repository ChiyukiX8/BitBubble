using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class MoneyUI : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _text;

    private void OnEnable()
    {
        // make text set based on currency manager event
        AppEvents.OnWealthUpdate.OnTrigger += OnWealthUpdated;
    }

    private void OnDisable()
    {
        AppEvents.OnWealthUpdate.OnTrigger -= OnWealthUpdated;
    }

    private void OnWealthUpdated(WealthData data)
    {
        SetMoneyValue(Mathf.RoundToInt(data.TotalValue));
    }

    private void SetMoneyValue(int value)
    {
        _text.text = value.ToString();
    }
}
