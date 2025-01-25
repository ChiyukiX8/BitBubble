using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class ColorSwatch : MonoBehaviour
{
    public Color Color => _image.color;

    [SerializeField]
    private Image _selectedFrame;

    private Button _button;
    private Image _image;
    private UnityAction<ColorSwatch> _onSelected;

    public void Setup(Color color, UnityAction<ColorSwatch> onSelected)
    {
        _image = GetComponent<Image>();
        _button = GetComponent<Button>();
        _button.onClick.AddListener(Select);

        _image.color = color;
        _selectedFrame.color = Color.Lerp(color, Color.white, 0.5f);
        _selectedFrame.gameObject.SetActive(false);
        _onSelected = null;
        _onSelected = onSelected;
    }

    public void SetSelected(bool condition)
    {
        _selectedFrame.gameObject.SetActive(condition);
    }

    public void Select()
    {
        _onSelected.Invoke(this);
    }
}
