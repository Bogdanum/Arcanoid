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
        _properties.Init(BlockRendererParamsID.Bedrock);
        var rendererParams = _properties.GetBlockRendererParamsByID(BlockRendererParamsID.Bedrock);
        mainSpriteRenderer.SetSprite(rendererParams.mainSprite);
        blockParticleSystem.SetColor(rendererParams.mainColor, rendererParams.accentColor);
        blockParticleSystem.SetSize(transform.localScale);
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
        yield return blockParticleSystem.PlayDestruction();
        onComplete?.Invoke();
    }
}
