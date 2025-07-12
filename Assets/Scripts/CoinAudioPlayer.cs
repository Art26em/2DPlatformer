using UnityEngine;

[RequireComponent(typeof(AudioSource))]

public class CoinAudioPlayer : MonoBehaviour
{
    private AudioSource _audioSource;
    
    private void OnEnable() => CoinController.OnCoinCollected += PlayCoinSound;
    private void OnDisable() => CoinController.OnCoinCollected -= PlayCoinSound;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }
    
    private void PlayCoinSound()
    {
        _audioSource.Play();
    }
}
