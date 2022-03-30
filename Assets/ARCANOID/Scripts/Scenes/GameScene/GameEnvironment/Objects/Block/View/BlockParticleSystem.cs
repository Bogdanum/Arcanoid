using System.Collections;
using UnityEngine;

public class BlockParticleSystem : MonoBehaviour
{
    [SerializeField] private ParticleSystem destructionParticles;
    [SerializeField] private ParticleSystem hitParticles;

    public void SetColor(Color mainColor, Color accentColor)
    {
        var destructionParticlesMain = destructionParticles.main;
        var settingsStartColor = destructionParticlesMain.startColor;
        settingsStartColor.mode = ParticleSystemGradientMode.TwoColors;
        settingsStartColor.colorMin = mainColor;
        settingsStartColor.colorMax = accentColor;
        destructionParticlesMain.startColor = settingsStartColor;

        if (hitParticles != null)
        {
            var hitParticlesMain = hitParticles.main;
            hitParticlesMain.startColor = mainColor;
        }
    }

    public void SetSize(Vector3 size)
    {
        destructionParticles.transform.localScale = size;
        if (hitParticles != null)
        {
            hitParticles.transform.localScale = size;   
        }
    }

    public IEnumerator PlayDestruction()
    {
        destructionParticles.Play();
        yield return new WaitUntil(() => !destructionParticles.isPlaying);
    }

    public void PlayHit()
    {
        hitParticles.Play();
    }
}
