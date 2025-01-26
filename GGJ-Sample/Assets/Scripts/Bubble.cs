using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class Bubble : MonoBehaviour, IPointerDownHandler
{
    public int Radius => radius;

    public UnityEvent<Bubble> OnBubblePopped = new UnityEvent<Bubble>();

    [Header("References")]
    [SerializeField]
    private GameObject pixelPrefab;
    [SerializeField]
    private GameObject reflectionPixelPrefab;
    [SerializeField]
    private Transform bubbleContainer;
    [SerializeField]
    private Transform iconContainer;
    [SerializeField]
    private Transform iconScalePivot;
    [SerializeField]
    private GameObject popParticlesPrefab;

    private List<BubblePixel> drawnBubblePixels = new List<BubblePixel>();
    private List<BubblePixel> drawnIconPixels = new List<BubblePixel>();

    public BubbleCreationConfig config;
    private float iconSizeFunction => (1f / 16f) * radius;
    private int radius = 10;
    private float radAccum;
    private CircleCollider2D bubbleCollider;

    private Color iconColor => config.Color;
    private Color bubbleColor => Color.Lerp(config.Color, Color.white, 0.5f);
    private Color reflectionColor => Color.Lerp(config.Color, Color.white, 0.9f);

    public void Setup(BubbleCreationConfig _config)
    {
        bubbleCollider = GetComponent<CircleCollider2D>();
        config = new BubbleCreationConfig(_config);
        // Determine radius from config.InitialValue
        DrawBubble(radius);
        DrawIcon(_config.Icon);

        AppEvents.OnCoinUpdate.OnTrigger += OnCoinUpdated;
    }

    private void OnDestroy()
    {
        AppEvents.OnCoinUpdate.OnTrigger -= OnCoinUpdated;
    }

    public void Pop()
    {
        Instantiate(popParticlesPrefab, transform.position, Quaternion.identity, null);

        // Invoke event, causes all pixels to get pushed outward from center
        OnBubblePopped?.Invoke(this);

        gameObject.SetActive(false);
        Destroy(gameObject, 5.0f);
    }

    private void DrawBubble(int radius)
    {
        if (radius == 0) return;

        // Clear all existing pixels, pop from end of list
        for(int i = drawnBubblePixels.Count - 1; i >= 0; i--)
        {
            Destroy(drawnBubblePixels[i].gameObject);
        }
        drawnBubblePixels.Clear();

        // Copied from ChatGPT ;)
        for (int x = -radius; x <= radius; x++)
        {
            for (int y = -radius; y <= radius; y++)
            {
                if (Mathf.RoundToInt(Mathf.Sqrt(x * x + y * y)) == radius)
                {
                    SpawnPixel(pixelPrefab, bubbleContainer, x, y, bubbleColor, ref drawnBubblePixels);
                }
            }
        }

        int reflectionRadius = Mathf.RoundToInt(radius * 0.5f);
        for (int x = 0; x <= reflectionRadius; x++)
        {
            for (int y = 0; y <= reflectionRadius; y++)
            {
                if (Mathf.RoundToInt(Mathf.Sqrt(x * x + y * y)) == reflectionRadius)
                {
                    SpawnPixel(reflectionPixelPrefab, bubbleContainer, x, y, reflectionColor, ref drawnBubblePixels);
                }
            }
        }

        bubbleCollider.radius = radius + 2f;
    }

    private void DrawIcon(HashSet<Vector2Int> iconInfo)
    {
        if(drawnIconPixels.Count == 0)
        {
            foreach (Vector2Int pos in iconInfo)
            {
                SpawnPixel(reflectionPixelPrefab, iconContainer, pos.x, pos.y, iconColor, ref drawnIconPixels, -1);
            }
        }
        iconScalePivot.localScale = new Vector3(iconSizeFunction, iconSizeFunction, 1);
    }

    private void SpawnPixel(GameObject prefab, Transform parent, int x, int y, Color color, ref List<BubblePixel> collection, int order = 0)
    {
        Vector3 spawnPosition = new Vector3(x, y, 0);
        GameObject spawnedPixel = Instantiate(prefab, parent);
        spawnedPixel.transform.localPosition = spawnPosition;
        SpriteRenderer sprite = spawnedPixel.GetComponentInChildren<SpriteRenderer>(true);
        sprite.sortingOrder = order;
        sprite.color = color;
        spawnedPixel.name = $"({spawnedPixel.transform.position.x},{spawnedPixel.transform.position.y}";

        BubblePixel bubblePixel = spawnedPixel.GetComponent<BubblePixel>();
        bubblePixel.Setup(this);
        collection.Add(bubblePixel);
    }

    private void OnCoinUpdated(CoinData data)
    {
        if (config.Id.Equals(data.Id))
        {
            radAccum += data.UpgradeSum();
            if (radAccum > 100)
            {
                radAccum = 0.0f;
                radius += 1;
                DrawBubble(radius);
                DrawIcon(config.Icon);
            }
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        BubbleUpgradeMenu.Instance.OpenBubbleMenu(config.Id);
        
        // Have camera pan/zoom to bubble
        AppEvents.OnBubbleClick.Trigger(config.Id);
    }
}
