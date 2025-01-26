using System;
using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class GameplayCanvas : MonoBehaviour
{
    public static GameplayCanvas Instance;
    public static CanvasScaler Scaler => Instance._scaler;

    [SerializeField]
    private UIMenuAnimationController _topShelf;
    [SerializeField]
    private GameObject _moneyIconPrefab;
    [SerializeField]
    private RectTransform _moneyRect;
    [SerializeField]
    private RectTransform _animatedIconsContainer;
    [SerializeField]
    private Button _closeGameButton;
    [SerializeField]
    private CanvasScaler _scaler;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }

        _closeGameButton.onClick.AddListener(OnCloseGameButtonClicked);
    }

    // Start is called before the first frame update
    void Start()
    {
        _topShelf.gameObject.SetActive(false);
        _topShelf.Show();
        _topShelf.GetComponentInChildren<TextMeshProUGUI>().mesh.RecalculateBounds();
    }

    public void InitiateMoneyTransfer(Bubble bubble, float value)
    {
        int coinCount = Mathf.RoundToInt(Mathf.Clamp(value / 1000, 1, Mathf.Infinity));

        StartCoroutine(CoMoneyTransfer(bubble.transform.position, bubble.Radius, coinCount));
    }
    private IEnumerator CoMoneyTransfer(Vector3 origin, float radius, int coinCount)
    {
        int spawnedCoins = 0;
        StartCoroutine(SoundEffectUpdate(coinCount * 0.01f));
        while(spawnedCoins < coinCount)
        {
            float randX = UnityEngine.Random.Range(-radius, radius);
            float randY = UnityEngine.Random.Range(-radius, radius);
            Vector3 randomSpawnPosInRadius = Camera.main.WorldToScreenPoint(origin + new Vector3(randX, randY, 0));
            GameObject spawnedCoin = Instantiate(_moneyIconPrefab, randomSpawnPosInRadius, Quaternion.identity, _animatedIconsContainer);
            spawnedCoin.transform.localEulerAngles = new Vector3(0, 0, UnityEngine.Random.Range(0, 360));
            MoneyIconAnimationController animCtrl = spawnedCoin.GetComponent<MoneyIconAnimationController>();

            float lifeTime = UnityEngine.Random.Range(0.8f, 1.2f);
            float scale = UnityEngine.Random.Range(0.8f, 1.2f);
            animCtrl.Animate(lifeTime, scale, _moneyRect.anchoredPosition);
            spawnedCoins++;

            // Spawn half, up to max of 50 of them initially
            if (spawnedCoins > Mathf.Min(coinCount / 2f, 50))
            {
                yield return new WaitForSeconds(0.01f);
            }
        }
    }

    private IEnumerator SoundEffectUpdate(float lifetime)
    {
        float lifetimeTimer = 0.0f;
        float soundEffectTimer = 0.0f;
        float soundEffectInterval = 0.2f;
        while (lifetimeTimer <= lifetime)
        {
            if (soundEffectTimer > soundEffectInterval)
            {
                soundEffectTimer = 0.0f;
                GlobalAudioSource.PlayAudioClipGroup(AudioClips.Instance.MoneyDepositSFX, Constants.UI_SFX_VOLUME_MODIFER);
            }
            yield return new WaitForEndOfFrame();
            lifetimeTimer += Time.deltaTime;
            soundEffectTimer += Time.deltaTime;
        }
    }

    private void OnCloseGameButtonClicked()
    {
        Application.Quit();
    }
}
