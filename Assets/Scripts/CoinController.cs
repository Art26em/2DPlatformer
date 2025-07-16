using System;
using UnityEngine;

public class CoinController : MonoBehaviour
{
    public static event Action OnCoinCollected;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerController>())
        {
            OnCoinCollected?.Invoke();
        }
    }
}
