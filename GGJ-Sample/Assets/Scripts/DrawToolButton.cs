using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DrawToolButton : MonoBehaviour
{
    [SerializeField]
    private DrawState _type;
    [SerializeField]
    private GameObject _selectedFrame;

    private Button _button;

    private void Awake()
    {
        _button = GetComponent<Button>();
        _button.onClick.AddListener(OnButtonClicked);
        DrawingZone.OnToolChanged.AddListener(OnToolChanged);
    }

    private void OnButtonClicked()
    {
        DrawingZone.OnToolChanged.Invoke(_type);
    }

    private void Select()
    {
        _selectedFrame.SetActive(true);
    }
    private void Deselect()
    {
        _selectedFrame.SetActive(false);
    }

    private void OnToolChanged(DrawState newTool)
    {
        if(_type != newTool)
        {
            Deselect();
        }
        else
        {
            Select();
        }
    }
}
