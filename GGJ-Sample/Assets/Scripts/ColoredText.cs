using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ColoredText : ColoredElement
{
    private TextMeshProUGUI _text;
    public override void SetAdjustedColor(Color color)
    {
        if (_text == null)
        {
            _text = GetComponent<TextMeshProUGUI>();
        }

        _text.color = GetAdjustedColor(color);
    }
}
