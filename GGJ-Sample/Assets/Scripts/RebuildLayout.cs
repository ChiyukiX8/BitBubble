using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RebuildLayout : MonoBehaviour
{
    public bool Setup => _setup;
    private LayoutGroup[] _layoutGroups;

    private bool _setup = false;

    private void Start()
    {
        Rebuild();
        _setup = true;
    }

    public void Rebuild()
    {
        if(_layoutGroups == null || _layoutGroups.Length == 0)
        {
            _layoutGroups = GetComponentsInChildren<LayoutGroup>(true);
        }
        if(_setup)
        {
            foreach (LayoutGroup group in _layoutGroups)
            {
                LayoutRebuilder.ForceRebuildLayoutImmediate(group.gameObject.transform as RectTransform);
            }
        }
    }
}
