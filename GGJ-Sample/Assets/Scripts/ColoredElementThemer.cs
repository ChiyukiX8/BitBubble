using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColoredElementThemer : MonoBehaviour
{
    private ColoredElement[] _coloredElements;
    public void SetColorTheme(Color color)
    {
        if(_coloredElements == null || _coloredElements.Length == 0)
        {
            _coloredElements = GetComponentsInChildren<ColoredElement>(true);
        }
        foreach(ColoredElement element in _coloredElements)
        {
            element.SetAdjustedColor(color);
        }
    }
}
