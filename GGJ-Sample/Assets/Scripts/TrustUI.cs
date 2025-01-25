using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TrustUI : MonoBehaviour
{
    [SerializeField]
    private Slider _slider;

    [SerializeField]
    private Image _sliderBackground;

    [SerializeField]
    private Image _sliderFill;

    [SerializeField]
    private Gradient _gradient;

    private void Awake()
    {
        // make trust value set based on GameManager action
    }

    private void SetTrustValue(float value)
    {
        _slider.value = value;

        Color gradientColor = _gradient.Evaluate(value);
        _sliderFill.color = gradientColor;
        _sliderBackground.color = Color.Lerp(gradientColor, Color.black, 0.33f);
    }
}
