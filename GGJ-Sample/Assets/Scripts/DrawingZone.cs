using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class DrawingZone : MonoBehaviour
{
    public static DrawingZone Instance;

    public static UnityEvent<HashSet<Vector2Int>> OnIconEdited = new UnityEvent<HashSet<Vector2Int>>();
    public static UnityEvent<Vector2Int, DrawState> OnPixelStateChanged = new UnityEvent<Vector2Int, DrawState>();
    public static UnityEvent<DrawState> OnToolChanged = new UnityEvent<DrawState>();
    public static DrawState CurrentTool { private set; get; }
    [SerializeField]
    private GameObject _pixelPrefab;
    [SerializeField]
    private Transform _pixelContainer;

    private HashSet<Vector2Int> _pixels = new HashSet<Vector2Int>();
    private Dictionary<Vector2Int, DrawnPixel> _spawnedPixels = new Dictionary<Vector2Int, DrawnPixel>();
    private bool _initialized = false;

    public void Initialize()
    {
        if (_initialized) return;

        if (Instance == null)
        {
            Instance = this;

        }
        SetupPixels(10);

        OnPixelStateChanged.AddListener(OnPixelDrawStateChanged);
        OnToolChanged.AddListener(ChangeTool);
        OnToolChanged.Invoke(DrawState.Drawn);
        _initialized = true;
    }
    public void ClearPixels()
    {
        foreach(Vector2Int pos in _spawnedPixels.Keys)
        {
            _spawnedPixels[pos].Hide();
        }
        _pixels.Clear();
    }
    private void SetupPixels(int size)
    {
        for(int x = 0; x < size; x++)
        {
            for(int y = 0; y < size; y++)
            {
                SpawnPixel(new Vector2Int(x, y));
            }
        }
    }
    private void SpawnPixel(Vector2Int position)
    {
        GameObject spawnedPixel = Instantiate(_pixelPrefab, _pixelContainer);
        DrawnPixel pixel = spawnedPixel.GetComponent<DrawnPixel>();
        pixel.Setup(position);
        _spawnedPixels.Add(position, pixel);
    }

    public void OnPixelDrawStateChanged(Vector2Int position, DrawState state)
    {
        if(state == DrawState.Drawn)
        {
            // add pixel from set
            _pixels.Add(position);
        }
        else if(state == DrawState.Invisible)
        {
            // remove pixel from set
            _pixels.Remove(position);
        }

        OnIconEdited.Invoke(_pixels);
    }

    private void ChangeTool(DrawState tool)
    {
        CurrentTool = tool;
    }
}
