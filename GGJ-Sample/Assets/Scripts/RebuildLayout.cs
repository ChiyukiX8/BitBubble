using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RebuildLayout : MonoBehaviour
{
    private List<RectTransform> _rects = new List<RectTransform>();

    private void Start()
    {
        Rebuild();
    }

    public void Rebuild()
    {
        if (_rects == null || _rects.Count == 0)
        {
            foreach (LayoutGroup layout in GetComponentsInChildren<LayoutGroup>(true))
            {
                RectTransform rect = layout.transform as RectTransform;
                if (_rects.Contains(rect) == false)
                {
                    _rects.Add(rect);
                }
            }
            foreach (ContentSizeFitter sizeFitter in GetComponentsInChildren<ContentSizeFitter>(true))
            {
                RectTransform rect = sizeFitter.gameObject.transform as RectTransform;
                if (_rects.Contains(rect) == false)
                {
                    _rects.Add(rect);
                }
            }
        }
        foreach (RectTransform rect in _rects)
        {
            LayoutRebuilder.ForceRebuildLayoutImmediate(rect);
        }
    }
}
