using System;
using UnityEngine;

public class BombExplosionParticles : PoolItem
{
    [SerializeField] private ParticleSystem explosionParticles;
    public Action OnComplete;

    private void OnParticleSystemStopped() => OnComplete?.Invoke();
    
    public void Play()
    {
        explosionParticles.Play();
    }
}
