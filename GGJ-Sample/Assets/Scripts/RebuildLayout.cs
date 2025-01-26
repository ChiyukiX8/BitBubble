using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RebuildLayout : MonoBehaviour
{
    private void OnEnable()
    {
        LayoutRebuilder.ForceRebuildLayoutImmediate(transform as RectTransform);
    }

    private void Awake()
    {
        LayoutRebuilder.ForceRebuildLayoutImmediate(transform as RectTransform);
    }

    private void Start()
    {
        LayoutRebuilder.ForceRebuildLayoutImmediate(transform as RectTransform);
    }
}
