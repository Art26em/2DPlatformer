using UnityEngine;

public class CoinDetector : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<CoinController>())
        {
            CollectCoin(collision.gameObject);
        }
    }

    private void CollectCoin(GameObject coin)
    {
        Destroy(coin);
    }
}
