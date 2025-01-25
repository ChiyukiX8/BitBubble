using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class MoneyUI : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _text;

    private void Awake()
    {
        // make text set based on game manager action
    }

    private void SetMoneyValue(int value)
    {
        _text.text = value.ToString();
    }
}
