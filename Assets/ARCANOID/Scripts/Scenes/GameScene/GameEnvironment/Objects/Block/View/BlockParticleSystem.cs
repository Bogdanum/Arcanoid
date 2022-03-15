using System.Collections;
using UnityEngine;

public class BlockParticleSystem : MonoBehaviour
{
    [SerializeField] private ParticleSystem particles;

    public void SetColor(Color mainColor, Color accentColor)
    {
        var settings = particles.main;
        var settingsStartColor = settings.startColor;
        settingsStartColor.mode = ParticleSystemGradientMode.TwoColors;
        settingsStartColor.colorMin = mainColor;
        settingsStartColor.colorMax = accentColor;
        settings.startColor = settingsStartColor;
    }

    public void SetSize(Vector3 size)
    {
        transform.localScale = size;
    }

    public IEnumerator Play()
    {
        particles.Play();
        yield return new WaitUntil(() => !particles.isPlaying);
    }
}
