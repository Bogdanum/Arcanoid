using System.Collections;
using UnityEngine;

public abstract class PopupAnimationController : MonoBehaviour
{
    public abstract void Init();
    public abstract IEnumerator ShowAnimation();
    public abstract IEnumerator HideAnimation();
}
