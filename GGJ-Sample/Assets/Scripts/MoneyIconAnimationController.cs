using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyIconAnimationController : MonoBehaviour
{
    private bool animating = false;
    public void Animate(float lifetime, float scale, Vector3 targetPosition)
    {
        StartCoroutine(CoAnimate(lifetime, scale, targetPosition));
    }
    public IEnumerator CoAnimate(float lifetime, float scale, Vector3 targetPosition)
    {
        StartCoroutine(ScaleUpdate(lifetime, scale));
        yield return StartCoroutine(PositionUpdate(lifetime, targetPosition));

        Destroy(gameObject);
    }
    private IEnumerator ScaleUpdate(float lifetime, float scale)
    {
        // Scale up to scale, then back to 0 over lifetime
        float timer = 0.0f;
        while(timer <= lifetime)
        {
            if(timer / lifetime < 0.25f)
            {
                transform.localScale = Vector3.Lerp(Vector3.zero, Vector3.one * scale, (timer / lifetime) / 0.25f);
            }

            if (timer / lifetime > 0.25f)
            {
                transform.localScale = Vector3.Lerp(Vector3.one * scale, Vector3.zero, ((timer / lifetime) - 0.25f) / 0.75f);
            }

            yield return new WaitForEndOfFrame();
            timer += Time.deltaTime;
        }
    }
    public IEnumerator PositionUpdate(float lifetime, Vector3 targetPosition)
    {
        float timer = 0.0f;
        RectTransform rect = transform as RectTransform;
        float adjustedLifetime = lifetime * 0.75f;
        while (timer <= lifetime)
        {
            if(timer / lifetime > 0.25f)
            {
                // Don't move for the first 1/4 of the lifetime
                float adjustedTimer = timer - (0.25f * lifetime);
                rect.anchoredPosition = Vector3.Lerp(rect.anchoredPosition, targetPosition, adjustedTimer / adjustedLifetime);
            }

            yield return new WaitForEndOfFrame();
            timer += Time.deltaTime;
        }
        animating = false;
    }
}
