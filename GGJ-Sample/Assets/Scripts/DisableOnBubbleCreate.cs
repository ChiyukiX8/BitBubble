using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableOnBubbleCreate : MonoBehaviour
{
    private void OnEnable()
    {
        AppEvents.OnCoinCreation.OnTrigger += OnBubbleCreated;
    }

    private void OnDisable()
    {
        AppEvents.OnCoinCreation.OnTrigger -= OnBubbleCreated;
    }

    private void OnBubbleCreated(BubbleCreationConfig config)
    {
        gameObject.SetActive(false);
    }
}
