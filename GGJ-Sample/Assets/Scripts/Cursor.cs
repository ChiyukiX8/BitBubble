using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cursor : MonoBehaviour
{
    [SerializeField]
    private GameObject _clickFXPrefab;
    [SerializeField]
    private SFXAudioSource _audioSource;
    [SerializeField]
    private Animator _cursorAnimator;

    private Bubble _bubbleTarget;
    public void Click()
    {
        GameObject spawnedClickFX = Instantiate(_clickFXPrefab, transform.position, Quaternion.identity, null);

        // Play audio
        _audioSource.PlayLocal(AudioClips.Instance.ClickSFX);
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
    public void SetAngry()
    {
        _cursorAnimator.SetTrigger("Angry");
        gameObject.transform.SetParent(null);
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
            
            if(_bubbleTarget == null)
            {
                StartCoroutine(WanderAngry(Random.Range(1f, 2f), Random.Range(3f, 5f)));
                yield break;
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
            if (_bubbleTarget == null) { yield break; }
            timer += Time.deltaTime;
        }
        // wait time before next
        yield return new WaitForSeconds(waitTime);
    }

    private IEnumerator WanderAngry(float stepDuration, float angryDuration)
    {
        float lifetimeTimer = 0.0f;
        float stepTimer = 0.0f;
        Vector3 targetPosition = transform.position + new Vector3(Random.Range(-15f, 15f), Random.Range(-15f, 15f), 0);
        while (lifetimeTimer < angryDuration)
        {
            transform.position = Vector3.Lerp(transform.position, targetPosition, stepTimer / stepDuration);
            yield return new WaitForEndOfFrame();
            lifetimeTimer += Time.deltaTime;
            stepTimer += Time.deltaTime;
            if(stepTimer > stepDuration)
            {
                stepTimer = 0.0f;
                // Calcualte new random position
                targetPosition = transform.position + new Vector3(Random.Range(-15f, 15f), Random.Range(-15f, 15f), 0);
                stepDuration = Mathf.Clamp(stepDuration - 0.2f, 0.5f, 2f);
            }
        }
        Destroy(gameObject);
    }
}
