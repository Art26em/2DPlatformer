using System.Collections.Generic;
using UnityEngine;

public class CoinSpawner : MonoBehaviour
{
    [SerializeField] private GameObject coinTemplate; 
    [SerializeField] private float yCoinPositionOffset;
    [SerializeField] private List<Transform> platformsToSpawnCoins;
    
    private void Start()
    {
        SpawnCoins();    
    }

    private void SpawnCoins()
    {
        foreach (var platformTransform in platformsToSpawnCoins)
        {
            var platformPos = platformTransform.position;
            Instantiate(coinTemplate, new Vector3(
                platformPos.x, 
                platformPos.y + yCoinPositionOffset, 
                platformPos.z)
                ,Quaternion.identity);
        }
    }
    
}
