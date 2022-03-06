using System.Collections;
using UnityEngine;

public class BlockParticleSystem : MonoBehaviour
{
    [SerializeField] private ParticleSystem particleSystem;

    public void SetColor(Color color)
    {
        var settings = particleSystem.main;
        settings.startColor = color;
    }

    public IEnumerator Play()
    {
        particleSystem.Play();
        yield return new WaitUntil(() => !particleSystem.isPlaying);
    }
}
