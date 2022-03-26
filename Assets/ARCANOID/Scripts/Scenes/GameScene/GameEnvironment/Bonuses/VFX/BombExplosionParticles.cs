using System;
using UnityEngine;

public class BombExplosionParticles : PoolItem, IPauseHandler
{
    [SerializeField] private ParticleSystem explosionParticles;
    public Action OnComplete;

    private void OnEnable() => MessageBus.Subscribe(this);
    private void OnDisable() => MessageBus.Unsubscribe(this);
    private void OnParticleSystemStopped() => OnComplete?.Invoke();
    public void OnGamePaused()
    {
        explosionParticles.Pause();
    }

    public void OnGameResumed() => Play();
    
    public void Play()
    {
        explosionParticles.Play();
    }

   
}
