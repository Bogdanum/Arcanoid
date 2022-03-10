using System;
using System.Collections;
using UnityEngine;
using Zenject;

public class PauseController : MonoBehaviour, IResumeButtonHandler
{
    [Inject] private PopupsManager _popupsManager;

    private void OnEnable() => MessageBus.Subscribe(this);
    private void OnDisable() => MessageBus.Unsubscribe(this);

    public void Pause()
    {
        MessageBus.RaiseEvent<IPauseHandler>(handler => handler.OnGamePaused());
        MessageBus.RaiseEvent<IInputBlockingHandler>(handler => handler.OnInputBlock());
        StartCoroutine(_popupsManager.Show<PausePopup>());
    }

    public void OnResumeButtonClicked() => StartCoroutine(ResumeProcess());

    private IEnumerator ResumeProcess()
    {
        yield return _popupsManager.HideAll();
        MessageBus.RaiseEvent<IPauseHandler>(handler => handler.OnGameResumed());
        MessageBus.RaiseEvent<IInputBlockingHandler>(handler => handler.OnInputActivation());
    }
}
