using UnityEngine;
using UnityEngine.UI;

public class ProgressSliderView : ProgressBarView
{
    [SerializeField] private Slider progressSlider;
    
    public override void SetMinValue(float value)
    {
        progressSlider.minValue = value;
    }

    public override void SetMaxValue(float value)
    {
        progressSlider.maxValue = value;
    }
    
    public override void UpdateProgress(float value)
    {
        progressSlider.value = value;
        CurrentProgress = value;
    }

    public override void ResetProgress()
    {
        progressSlider.value = progressSlider.minValue;
        CurrentProgress = progressSlider.value;
    }
}
