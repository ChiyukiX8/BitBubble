using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class UIMenuAnimationController : MonoBehaviour
{
    public enum EnterDirection { Up, Down };
    public float duration = 1.0f;
    public EnterDirection direction = EnterDirection.Up;

    private RectTransform rect => transform as RectTransform;
    private bool _originalPositionStored = false;
    private Vector3 _originalPosition;
    private UnityEvent _onHidden = new UnityEvent();
    private UnityEvent _onShown = new UnityEvent();

    private bool _animating = false;

    private void Awake()
    {
        _onHidden.AddListener(OnHidden);
        _onShown.AddListener(OnShown);
    }

    private void OnEnable()
    {
        StoreOriginalPosition();
    }

    public void Show()
    {
        if(_animating)
        {
            StopAllCoroutines();
        }
        gameObject.SetActive(true);

        StoreOriginalPosition();

        rect.anchoredPosition = GetOffScreenPosition();

        StartCoroutine(CoShow());
    }

    private IEnumerator CoShow()
    {
        _animating = true;
        float timer = 0.0f;
        while (timer <= duration)
        {
            rect.anchoredPosition = Vector3.Lerp(rect.anchoredPosition, _originalPosition, timer / duration);
            yield return new WaitForEndOfFrame();
            timer += Time.deltaTime;
        }
        rect.anchoredPosition = _originalPosition;
        _animating = false;
        _onShown.Invoke();
    }

    private void OnShown()
    {
        
    }

    public void Hide()
    {
        if(_animating)
        {
            StopAllCoroutines();
        }

        StartCoroutine(CoHide());
    }

    private IEnumerator CoHide()
    {
        _animating = true;
        float timer = 0.0f;
        while (timer <= duration)
        {
            rect.anchoredPosition = Vector3.Lerp(rect.anchoredPosition, GetOffScreenPosition(), timer / duration);
            yield return new WaitForEndOfFrame();
            timer += Time.deltaTime;
        }
        rect.anchoredPosition = GetOffScreenPosition();
        _animating = false;
        _onHidden.Invoke();
    }

    public void AddOnShownListener(UnityAction action)
    {
        _onShown.AddListener(action);
    }
    public void RemoveOnShownListener(UnityAction action)
    {
        _onShown.RemoveListener(action);
    }
    public void AddOnHiddenListener(UnityAction action)
    {
        _onHidden.AddListener(action);
    }
    public void RemoveOnHiddenListener(UnityAction action)
    {
        _onHidden.RemoveListener(action);
    }

    private void OnHidden()
    {
        gameObject.SetActive(false);
    }
    private void StoreOriginalPosition()
    {
        if (!_originalPositionStored)
        {
            _originalPosition = rect.anchoredPosition;
            _originalPositionStored = true;
        }
    }

    private Vector3 GetOffScreenPosition()
    {
        return _originalPosition - new Vector3(0, direction == EnterDirection.Up ? GameplayCanvas.Scaler.referenceResolution.y : -GameplayCanvas.Scaler.referenceResolution.y, 0);
    }
}
