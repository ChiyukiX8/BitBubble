using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class GameplayCanvas : MonoBehaviour
{
    [SerializeField]
    private UIMenuAnimationController _topShelf;
    // Start is called before the first frame update
    void Start()
    {
        _topShelf.gameObject.SetActive(false);
        _topShelf.Show();
        _topShelf.GetComponentInChildren<TextMeshProUGUI>().mesh.RecalculateBounds();
    }
}
