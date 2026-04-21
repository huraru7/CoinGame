using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Pool;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public int coinCount = 50;
    [SerializeField] private GameObject coinPrefab;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private int poolDefaultSize = 10;
    [SerializeField] private int poolMaxSize = 50;

    private ObjectPool<GameObject> coinPool;

    void Awake()
    {
        Instance = this;
        coinPool = new ObjectPool<GameObject>(
            createFunc: () => Instantiate(coinPrefab),
            actionOnGet: obj => obj.SetActive(true),
            actionOnRelease: obj => obj.SetActive(false),
            actionOnDestroy: obj => Destroy(obj),
            defaultCapacity: poolDefaultSize,
            maxSize: poolMaxSize
        );
    }

    void Update()
    {
        if (Keyboard.current[Key.Space].wasPressedThisFrame && coinCount > 0)
        {
            CoinSpawn();
        }
    }

    void CoinSpawn()
    {
        GameObject coin = coinPool.Get();
        coin.transform.SetPositionAndRotation(spawnPoint.position, Quaternion.identity);
        coinCount--;
    }

    public void ReleaseCoin(GameObject coin)
    {
        coinPool.Release(coin);
    }

    public void AddCoin()
    {
        coinCount++;
    }
}
