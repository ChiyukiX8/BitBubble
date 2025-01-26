using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ColoredElement : MonoBehaviour
{
    public float BaseValue = 1.0f;

    public abstract void SetAdjustedColor(Color color);

    protected Color GetAdjustedColor(Color color)
    {
        Color.RGBToHSV(color, out float h, out float s, out float v);
        return Color.HSVToRGB(h, s, BaseValue);
    }
}
