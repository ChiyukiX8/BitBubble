using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class CreateBubblePanel : MonoBehaviour
{
    public static CreateBubblePanel Instance;

    private const string DEFAULT_NAME = "New Currency Name";

    [SerializeField]
    private GameObject _bubbleMenu;
    [SerializeField]
    private Button _openMenuButton;
    [SerializeField]
    private GameObject _bubblePrefab;
    [SerializeField]
    private DrawingZone _drawingZone;
    [SerializeField]
    private ColorSelector _colorSelector;
    [SerializeField]
    private TMP_InputField _nameField;
    [SerializeField]
    private Slider _valueSlider;
    [SerializeField]
    private TextMeshProUGUI _valueText;
    [SerializeField]
    private Button _confirmButton;
    [SerializeField]
    private Button _closeButton;

    private BubbleCreationConfig _currentConfig = new BubbleCreationConfig();

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }

        _nameField.onValueChanged.AddListener(SetName);
        _valueSlider.onValueChanged.AddListener(SetValue);
        _confirmButton.onClick.AddListener(OnConfirmButtonClicked);
        _closeButton.onClick.AddListener(OnCloseButtonClicked);
        _openMenuButton.onClick.AddListener(Open);
        ColorSelector.OnColorChanged.AddListener(SetColor);
        DrawingZone.OnIconEdited.AddListener(SetIcon);

        _colorSelector.Initialize();
        ClearConfig();

        _drawingZone.Initialize();
    }

    private void Update()
    {
        _confirmButton.interactable = Valid();
    }

    public void CreateBubble(BubbleCreationConfig config)
    {
        Vector3 spawnPosition = new Vector3(0.01f, 0, 0);
        // Try to spawn bubble in center of screen, if a bubble exists here...
        // Pick a random angle around that bubble and spawn this bubble the combined radius distance away
        Ray ray = new Ray(new Vector3(0, 0, -1), new Vector3(0, 0, 1));
        RaycastHit2D hit = Physics2D.GetRayIntersection(ray, 10);
        if (hit.collider != null)
        {
            spawnPosition = hit.collider.transform.position + new Vector3(UnityEngine.Random.Range(-1f, 1f), UnityEngine.Random.Range(-1f, 1f), 0);
            spawnPosition.Normalize();
            spawnPosition *= hit.collider.gameObject.GetComponent<Bubble>().Radius + 10f + 2f;
            spawnPosition.z = 0;
        }

        // Probably want to reach out to game manager to get a parent we can put the bubbles under
        GameObject spawnedBubble = Instantiate(_bubblePrefab, spawnPosition, Quaternion.identity, null);

        GlobalAudioSource.PlayAudioClipGroup(AudioClips.Instance.CreateBubbleSFX);

        spawnedBubble.GetComponent<Bubble>().Setup(config);
    }

    public void ClearConfig()
    {
        _colorSelector.SelectSwatch(0);
        _currentConfig = new BubbleCreationConfig(DEFAULT_NAME, 0, ColorSelector.SelectedColor, new HashSet<Vector2Int>());
        _nameField.text = "";
        _valueSlider.value = 0.0f;
        SetName("");
        SetValue(_currentConfig.InitialValue);
        SetColor(_currentConfig.Color);
        SetIcon(new HashSet<Vector2Int>());
        _drawingZone.ClearPixels();
    }

    public bool Valid()
    {
        bool nameValid = string.IsNullOrEmpty(_currentConfig.Name) == false;
        bool valueValid = _currentConfig.InitialValue > 0;
        bool iconValid = _currentConfig.Icon.Count > 0;
        return nameValid && valueValid && iconValid;
    }

    private void Open()
    {
        ClearConfig();
        // Pause game?
        _bubbleMenu.SetActive(true);
        _openMenuButton.gameObject.SetActive(false);
    }
    private void Close()
    {
        _bubbleMenu.SetActive(false);
        _openMenuButton.gameObject.SetActive(true);
    }

    private void SetName(string name)
    {
        _currentConfig.Name = name;
    }

    private void SetValue(float value)
    {
        _currentConfig.InitialValue = Mathf.RoundToInt(value * CurrencyManager.Instance.Wealth.TotalValue);
        _valueText.text = _currentConfig.InitialValue.ToString();
    }

    private void SetColor(Color color)
    {
        _currentConfig.Color = color;
    }

    private void SetIcon(HashSet<Vector2Int> icon)
    {
        _currentConfig.Icon = icon;
    }

    private void OnConfirmButtonClicked()
    {
        if(Valid())
        {
            _currentConfig.Id = Guid.NewGuid();
            // spawn Bubble
            CreateBubble(_currentConfig);
            CurrencyManager.Instance.Wealth.TotalValue -= _currentConfig.InitialValue;
            AppEvents.OnCoinCreation.Trigger(_currentConfig);
            Close();
        }
    }
    private void OnCloseButtonClicked()
    {
        Close();
    }
}

public class BubbleCreationConfig
{
    public Guid Id;
    public string Name;
    public int InitialValue;
    public Color Color;
    public HashSet<Vector2Int> Icon = new HashSet<Vector2Int>();

    public BubbleCreationConfig()
    {

    }
    public BubbleCreationConfig(BubbleCreationConfig copyFrom)
    {
        Id = copyFrom.Id;
        Name = copyFrom.Name;
        InitialValue = copyFrom.InitialValue;
        Color = copyFrom.Color;
        Icon = copyFrom.Icon;
    }
    public BubbleCreationConfig(string name, int value, Color color, HashSet<Vector2Int> icon)
    {
        Id = new Guid();
        Name = name;
        InitialValue = value;
        Color = color;
        Icon = icon;
    }
}