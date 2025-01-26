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

        _image.color = GetAdjustedColor(color);
    }
}
