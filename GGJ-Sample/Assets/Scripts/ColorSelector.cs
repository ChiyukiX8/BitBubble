using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ColorSelector : MonoBehaviour
{
    public static ColorSelector Instance = null;
    public static UnityEvent<Color> OnColorChanged = new UnityEvent<Color>();

    public static Color SelectedColor => Instance._selectedSwatch.Color;

    [Header("Options")]
    [SerializeField]
    private List<Color> _colorOptions;
    [Header("References")]
    [SerializeField]
    private Transform _container;
    [SerializeField]
    private GameObject _swatchPrefab;

    private List<ColorSwatch> _swatches = new List<ColorSwatch>();
    private ColorSwatch _selectedSwatch;
    private bool _initialized = false;

    public void Initialize()
    {
        if (_initialized) return;

        if (Instance == null)
        {
            Instance = this;
        }

        SetupSwatches();

        _initialized = true;
    }

    public void SelectSwatch(int index)
    {
        OnSwatchSelected(_swatches[index]);
    }

    private void SetupSwatches()
    {
        foreach(Color color in _colorOptions)
        {
            SpawnSwatch(color);
        }
    }

    private void SpawnSwatch(Color color)
    {
        GameObject spawnedSwatch = Instantiate(_swatchPrefab, _container);
        ColorSwatch swatch = spawnedSwatch.GetComponent<ColorSwatch>();
        swatch.Setup(color, OnSwatchSelected);
        _swatches.Add(swatch);
    }

    private void OnSwatchSelected(ColorSwatch swatch)
    {
        if(_selectedSwatch != null)
        {
            _selectedSwatch.SetSelected(false);
        }
        swatch.SetSelected(true);
        _selectedSwatch = swatch;

        OnColorChanged.Invoke(_selectedSwatch.Color);
    }
}
