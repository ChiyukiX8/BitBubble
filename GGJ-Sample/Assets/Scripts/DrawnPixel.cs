using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public enum DrawState { Drawn, Invisible }

public class DrawnPixel : MonoBehaviour,IPointerEnterHandler,IPointerDownHandler
{
    private Image _image;
    private Button _button;
    private DrawState _drawnState;
    private Vector2Int _position;

    public void Setup(Vector2Int position)
    {
        _image = GetComponent<Image>();
        _button = GetComponent<Button>();

        _position = position;
        ColorSelector.OnColorChanged.AddListener(SetColor);
        SetColor(ColorSelector.SelectedColor);
        Hide();
    }


    private void OnPixelClicked()
    {
        if(_drawnState == DrawingZone.CurrentTool)
        {
            return;
        }

        if(_drawnState == DrawState.Drawn)
        {
            Hide();
        }
        else
        {
            Draw();
        }

        DrawingZone.OnPixelStateChanged.Invoke(_position, _drawnState);
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        if(Input.GetMouseButton(0))
        {
            OnPixelClicked();
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        OnPixelClicked();
    }

    public void Draw()
    {
        _drawnState = DrawState.Drawn;
        _image.color = new Color(_image.color.r, _image.color.g, _image.color.b, 1.0f);
    }
    public void Hide()
    {
        _drawnState = DrawState.Invisible;
        _image.color = new Color(_image.color.r, _image.color.g, _image.color.b, 0.0f);
    }
    private void SetColor(Color color)
    {
        _image.color = new Color(color.r, color.g, color.b, _image.color.a);
    }
}
