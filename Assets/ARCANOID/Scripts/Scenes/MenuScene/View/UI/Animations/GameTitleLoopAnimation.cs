using DG.Tweening;
using UnityEngine;

public class GameTitleLoopAnimation : MonoBehaviour
{
    [SerializeField] private Vector3 minPosition;
    [SerializeField] private Vector3 maxPosition;
    [SerializeField] private float moveTime;
    [SerializeField] private Ease ease;
    [SerializeField] private LoopType loopType = LoopType.Yoyo;
    private Tween _tween;
    private bool _isPause = true;

    public void Play()
    {
        _tween = null;
        _isPause = false;
        StartLoopAnim();
    }

    public void Stop()
    {
        _tween?.Kill();
        _tween = null;
        _isPause = true;
    }

    private void StartLoopAnim()
    {
        if (_tween != null || _isPause) return;
        
        transform.localPosition = minPosition;
        _tween = transform.DOLocalMove(maxPosition, moveTime).SetEase(ease).SetLoops(-1, loopType);
    }
}
