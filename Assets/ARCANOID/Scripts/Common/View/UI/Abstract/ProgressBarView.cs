using UnityEngine;

public abstract class ProgressBarView : MonoBehaviour
{
    public float CurrentProgress { get; protected set; }
    public virtual void SetMinValue(float value) {}
    public virtual void SetMaxValue(float value) {}

    public abstract void UpdateProgress(float value);
    public abstract void ResetProgress();
}
       