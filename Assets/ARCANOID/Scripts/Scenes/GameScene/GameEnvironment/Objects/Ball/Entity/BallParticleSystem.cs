using UnityEngine;

public class BallParticleSystem : MonoBehaviour
{
    [SerializeField] private ParticleSystem normalParticles;
    [SerializeField] private ParticleSystem rageParticles;
    
    public void SetupParticlesColor(Color first, Color second)
    {
        var settings = normalParticles.main;
        var settingsStartColor = settings.startColor;
        settingsStartColor.mode = ParticleSystemGradientMode.TwoColors;
        settingsStartColor.colorMin = first;
        settingsStartColor.colorMax = second;
        settings.startColor = settingsStartColor;
    }

    public void PlayNormalParticles()
    {
        rageParticles.Stop();
        normalParticles.Play();
    }

    public void PlayRageParticles()
    {
        normalParticles.Stop();
        rageParticles.Play();
    }
}
