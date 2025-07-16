using UnityEngine;

public class CoinSpawner : MonoBehaviour
{
    [SerializeField] private GameObject coinTemplate; 
    [SerializeField] private float yCoinPositionOffset;
    
    private PlatformController[] _platforms;
    
    private void Start()
    {
        SpawnCoins();    
    }

    private void SpawnCoins()
    {
        _platforms = GameObject.FindObjectsOfType<PlatformController>();
        foreach (var t in _platforms)
        {
            var platformPos = t.transform.position;
            Instantiate(coinTemplate, new Vector3(platformPos.x, platformPos.y + yCoinPositionOffset, platformPos.z),Quaternion.identity);
        }
    }
    
}
