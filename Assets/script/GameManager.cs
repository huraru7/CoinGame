using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Pool;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public int coinCount { get; private set; } = 50;
    [SerializeField] private GameObject coinPrefab;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private int poolDefaultSize = 10;
    [SerializeField] private int poolMaxSize = 100;
    [SerializeField] private TMP_Text coinCountText;
    [SerializeField] private int startCoinCount = 20;
    [SerializeField] private Transform fieldCenter;
    [SerializeField] private Vector2 fieldSize = new Vector2(2f, 2f);

    private ObjectPool<GameObject> coinPool;

    void Start()
    {
        InitialCoinSpawn();
    }

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        coinPool = new ObjectPool<GameObject>(
            createFunc: () => Instantiate(coinPrefab),
            actionOnGet: obj => obj.SetActive(true),
            actionOnRelease: obj => obj.SetActive(false),
            actionOnDestroy: obj => Destroy(obj),
            defaultCapacity: poolDefaultSize,
            maxSize: poolMaxSize
        );
        UpdateCoinUI();
    }

    void Update()
    {
        if (Keyboard.current[Key.Space].wasPressedThisFrame && coinCount > 0)
        {
            CoinSpawn();
        }
    }

    void UpdateCoinUI()
    {
        coinCountText.text = $"coin: {coinCount}";
    }

    void InitialCoinSpawn()
    {
        for (int i = 0; i < startCoinCount; i++)
        {
            GameObject coin = coinPool.Get();
            float x = fieldCenter.position.x + Random.Range(-fieldSize.x / 2f, fieldSize.x / 2f);
            float z = fieldCenter.position.z + Random.Range(-fieldSize.y / 2f, fieldSize.y / 2f);
            coin.transform.SetPositionAndRotation(
                new Vector3(x, fieldCenter.position.y, z),
                Quaternion.identity
            );
        }
    }

    void CoinSpawn()
    {
        GameObject coin = coinPool.Get();
        coin.transform.SetPositionAndRotation(spawnPoint.position, Quaternion.identity);
        coinCount--;
        UpdateCoinUI();
    }

    public void ReleaseCoin(GameObject coin)
    {
        coinPool.Release(coin);
    }

    public void AddCoin()
    {
        coinCount++;
        UpdateCoinUI();
    }
}