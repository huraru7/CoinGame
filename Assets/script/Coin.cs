using UnityEngine;

public class Coin : MonoBehaviour
{
    //:落下判定用トリガーに "Goal" タグを付けたGameObjectを配置する
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Goal"))
        {
            GameManager.Instance.AddCoin();
            GameManager.Instance.ReleaseCoin(gameObject);
        }
    }
}