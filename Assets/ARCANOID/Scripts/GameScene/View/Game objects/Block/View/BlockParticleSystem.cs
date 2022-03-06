using System.Collections;
using UnityEngine;

public class BlockParticleSystem : MonoBehaviour
{
    [SerializeField] private ParticleSystem particles;

    public void SetColor(Color color)
    {
        var settings = particles.main;
        settings.startColor = color;
    }

    public IEnumerator Play()
    {
        particles.Play();
        yield return new WaitUntil(() => !particles.isPlaying);
    }
}
