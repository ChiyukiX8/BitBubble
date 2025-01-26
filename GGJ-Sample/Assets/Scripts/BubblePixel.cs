using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BubblePixel : MonoBehaviour
{
    private Bubble _ownerBubble;
    public void Setup(Bubble owner)
    {
        _ownerBubble = owner;
        _ownerBubble.OnBubblePopped.AddListener(PushFromOrigin);
    }

    private void OnDestroy()
    {
        if(_ownerBubble != null)
        {
            _ownerBubble.OnBubblePopped.RemoveListener(PushFromOrigin);
        }
    }

    private void PushFromOrigin(Bubble ownerBubble)
    {
        transform.SetParent(null);
        StartCoroutine(CoPushFromOrigin(ownerBubble.transform.position, Random.Range(0.0f, 0.25f), Random.Range(2f, 5f), ownerBubble.Radius + Random.Range(5f, 25f)));
    }

    private IEnumerator CoPushFromOrigin(Vector3 origin, float delay, float duration, float magnitude)
    {
        float timer = 0.0f;

        yield return new WaitForSeconds(delay);

        Vector3 targetPos = origin + ((transform.position - origin).normalized * magnitude);

        while(timer < duration)
        {
            transform.position = Vector3.Lerp(transform.position, targetPos, timer / duration);
            transform.localScale = Vector3.Lerp(transform.localScale, Vector3.zero, timer / duration);
            yield return new WaitForEndOfFrame();
            timer += Time.deltaTime;
        }
        Destroy(gameObject);
    }
}
