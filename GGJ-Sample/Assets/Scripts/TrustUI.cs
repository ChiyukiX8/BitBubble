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

    private void OnEnable()
    {
        LayoutRebuilder.ForceRebuildLayoutImmediate(transform as RectTransform);
    }

    private void Awake()
    {
        AppEvents.OnTrustUpdate.OnTrigger += TrustUpdated;
    }

    private void OnDestroy()
    {
        AppEvents.OnTrustUpdate.OnTrigger -= TrustUpdated;
    }

    private void TrustUpdated(TrustData data)
    {
        SetTrustValue(data.TotalValue);
    }

    private void SetTrustValue(float value)
    {
        _slider.value = value;

        Color gradientColor = _gradient.Evaluate(value);
        _sliderFill.color = gradientColor;
        _sliderBackground.color = Color.Lerp(gradientColor, Color.black, 0.33f);
    }
}
