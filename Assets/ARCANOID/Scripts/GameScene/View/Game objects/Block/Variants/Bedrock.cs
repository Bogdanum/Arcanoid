using System;
using System.Collections;

public class Bedrock : Block
{
    private BlocksDesignProperties _properties;
    
    public override void Init(BlocksDesignProperties designProps)
    {
        mainSpriteRenderer.RefreshScale();
        _properties = designProps;
        Type = BlockType.Bedrock;
    }

    public void SetInitialParams()
    {
        var rendererParams = _properties.GetBlockRendererParamsByID(BlockRendererParamsID.Bedrock);
        mainSpriteRenderer.SetSprite(rendererParams.mainSprite);
        particleSystem.SetColor(rendererParams.particlesColor);
    }

    public override void Destroy()
    {
        if (Destroyed) return;

        Destroyed = true;
        StartCoroutine(PlayParticlesAndDestroy(() =>
            MessageBus.RaiseEvent<IBlockLifecycleHandler>(handler => handler.OnBlockDestroyed(this))
        ));
    }

    private IEnumerator PlayParticlesAndDestroy(Action onComplete = null)
    {
        mainSpriteRenderer.Disable();
        yield return particleSystem.Play();
        onComplete?.Invoke();
    }
}
