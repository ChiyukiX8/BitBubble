using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cursor : MonoBehaviour
{
    [SerializeField]
    private GameObject _clickFXPrefab;

    private Bubble _bubbleTarget;
    public void Click()
    {
        GameObject spawnedClickFX = Instantiate(_clickFXPrefab, transform.position, Quaternion.identity, null);

        // Play audio
    }

    private void OnEnable()
    {
        
    }

    // Setting bubble as target
    public void SetTarget(Bubble bubble)
    {
        transform.position = GetRandomLocationInTarget(bubble);
        _bubbleTarget = bubble;
        StartCoroutine(Wander());
    }

    // Setting trust bar as target
    private void SetTarget(RectTransform rect)
    {

    }

    private Vector3 GetRandomLocationInTarget(Bubble bubble)
    {
        float innerSquareSize = (Mathf.Sqrt(2) * bubble.Radius) / 2;
        float randomX = bubble.transform.position.x + Random.Range(-innerSquareSize, innerSquareSize);
        float randomY = bubble.transform.position.y + Random.Range(-innerSquareSize, innerSquareSize);
        Vector3 randomPosition = new Vector3(randomX, randomY, 0);
        return randomPosition;
    }

    private IEnumerator Wander()
    {
        while(true)
        {
            float stepDuration = Random.Range(1f, 7.5f);
            float waitTime = Random.Range(1f, 5f);
            if (_bubbleTarget != null)
            {
                yield return WanderBubble(stepDuration, waitTime);
            }
            else
            {
                yield return WanderRect(stepDuration, waitTime);
            }
        }
    }

    private IEnumerator WanderBubble(float stepDuration, float waitTime)
    {
        float timer = 0.0f;
        Vector3 randomPos = GetRandomLocationInTarget(_bubbleTarget);
        while (timer < stepDuration)
        {
            transform.position = Vector3.Lerp(transform.position, randomPos, timer / stepDuration);
            yield return new WaitForEndOfFrame();
            timer += Time.deltaTime;
        }
        // wait time before next
        yield return new WaitForSeconds(waitTime);
    }

    private IEnumerator WanderRect(float stepDuration, float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
    }
}
