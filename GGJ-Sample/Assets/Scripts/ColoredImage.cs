using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColoredImage : ColoredElement
{
    private Image _image;
    public override void SetAdjustedColor(Color color)
    {
        if(_image == null)
        {
            _image = GetComponent<Image>();
        }

        Color adjustedColor = GetAdjustedColor(color);
        _image.color = new Color(adjustedColor.r, adjustedColor.g, adjustedColor.b, _image.color.a);
    }
}
