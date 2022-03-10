using System.Collections;
using UnityEngine;
using Zenject;

public class GameResultController : MonoBehaviour, IGameResultHandler
{
    [Inject] private PopupsManager _popupsManager;
    
    private void OnEnable() => MessageBus.Subscribe(this);
    private void OnDisable() => MessageBus.Unsubscribe(this);

    public void OnVictory() => StartCoroutine(Victory());

    private IEnumerator Victory()
    {
        yield return _popupsManager.Show<VictoryPopup>();
    }

    public void OnLose() => StartCoroutine(Lose());

    private IEnumerator Lose()
    {
        yield return _popupsManager.Show<LosePopup>();
    }
}
