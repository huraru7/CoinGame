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
    private InputSystem_Actions inputActions;

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
        inputActions = new InputSystem_Actions();
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

    void OnEnable()
    {
        inputActions.Player.Enable();
        inputActions.Player.Spawn.performed += OnSpawnPerformed;
    }

    void OnDisable()
    {
        inputActions.Player.Spawn.performed -= OnSpawnPerformed;
        inputActions.Player.Disable();
    }

    private void OnSpawnPerformed(InputAction.CallbackContext _)
    {
        if (coinCount > 0) CoinSpawn();
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
            float halfFieldX = fieldSize.x / 2f;
            float halfFieldZ = fieldSize.y / 2f;
            float x = fieldCenter.position.x + Random.Range(-halfFieldX, halfFieldX);
            float z = fieldCenter.position.z + Random.Range(-halfFieldZ, halfFieldZ);
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