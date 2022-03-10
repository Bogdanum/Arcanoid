using UnityEngine;

public class ProgressSliderCounter : MonoBehaviour
{
    [SerializeField] private ProgressBarView progressBarView;
    [SerializeField] private ValueChangeAnimation progressAnimation;

    private float _count;

    public void ResetProgressBar()
    {
        progressAnimation.Stop();
        progressBarView.ResetProgress();
    }

    public void InitProgressBar(float minValue, float maxValue)
    {
        _count = maxValue;
        progressBarView.SetMaxValue(_count);
        progressBarView.SetMinValue(minValue);
    }

    public void UpdateProgress(float value)
    {
        var start = progressBarView.CurrentProgress;
        var end = _count - value;
        progressAnimation.Play(start, end, UpdateView);
    }

    public void UpdateView(float value)
    {
        progressBarView.UpdateProgress(value);
    }
}
