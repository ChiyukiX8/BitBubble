using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class MoneyUI : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _text;

    [SerializeField]
    private RebuildLayout _rebuilder;

    private List<RectTransform> rects = new List<RectTransform>();
    private string _previousText = "";

    private void Start()
    {
        SetMoneyValue(Mathf.RoundToInt(CurrencyManager.Instance.Wealth.TotalValue));
    }

    private void OnEnable()
    {
        LayoutRebuilder.ForceRebuildLayoutImmediate(transform as RectTransform);

        // make text set based on currency manager event
        AppEvents.OnWealthUpdate.OnTrigger += OnWealthUpdated;
    }

    private void OnDisable()
    {
        AppEvents.OnWealthUpdate.OnTrigger -= OnWealthUpdated;
    }

    private void Update()
    {
        if(_previousText != _text.text)
        {
            Rebuild();
            _previousText = _text.text;
        }
    }

    private void OnWealthUpdated(WealthData data)
    {
        SetMoneyValue(Mathf.RoundToInt(data.TotalValue));
    }

    private void SetMoneyValue(int value)
    {
        _text.text = value.ToString();
    }

    private void Rebuild()
    {
        _rebuilder.Rebuild();
    }
}
