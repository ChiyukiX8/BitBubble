using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bubble : MonoBehaviour
{
    public int radius = 10;
    [Header("References")]
    [SerializeField]
    private GameObject pixelPrefab;
    [SerializeField]
    private Transform pixelContainer;

    private List<GameObject> drawnPixels = new List<GameObject>();

    private void Start()
    {
        Draw(radius);
    }

    private void Update()
    {
        // For testing sizes for now
        if(Input.GetKeyDown(KeyCode.Equals))
        {
            radius++;
            Draw(radius);
        }
        else if(Input.GetKeyDown(KeyCode.Minus))
        {
            radius--;
            Draw(radius);
        }
    }
    private void Draw(int radius)
    {
        if (radius == 0) return;

        // Clear all existing pixels, pop from end of list
        for(int i = drawnPixels.Count - 1; i >= 0; i--)
        {
            Destroy(drawnPixels[i]);
        }
        drawnPixels.Clear();

        // Copied from ChatGPT ;)
        for (int x = -radius; x <= radius; x++)
        {
            for (int y = -radius; y <= radius; y++)
            {
                if (Mathf.RoundToInt(Mathf.Sqrt(x * x + y * y)) == radius)
                {
                    SpawnPixel(x, y);
                }
            }
        }

        int reflectionRadius = Mathf.RoundToInt(radius * 0.6f);
        for (int x = -reflectionRadius; x <= reflectionRadius; x++)
        {
            for (int y = -reflectionRadius; y <= reflectionRadius; y++)
            {
                if (x <= 0 || y <= 0) continue;

                if (Mathf.RoundToInt(Mathf.Sqrt(x * x + y * y)) == reflectionRadius)
                {
                    SpawnPixel(x, y);
                }
            }
        }
    }

    private void SpawnPixel(int x, int y)
    {
        Vector3 spawnPosition = new Vector3(x, y, 0);
        GameObject spawnedPixel = Instantiate(pixelPrefab, spawnPosition, Quaternion.identity, pixelContainer);
        spawnedPixel.name = $"({spawnedPixel.transform.position.x},{spawnedPixel.transform.position.y}";
        drawnPixels.Add(spawnedPixel);
    }
}
