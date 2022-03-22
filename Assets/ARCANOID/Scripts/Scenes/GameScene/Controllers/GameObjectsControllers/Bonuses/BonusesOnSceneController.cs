using UnityEngine;
using Zenject;

public class BonusesOnSceneController : MonoBehaviour, IBonusLifecycleHandler, IClearGameFieldHandler
{
    [SerializeField] private Transform bonusesContainer;
    private DroppableBonusSpawner _droppableBonusSpawner;
    
    [Inject]
    public void Init(DroppableBonusSpawner droppableBonusSpawner)
    {
        _droppableBonusSpawner = droppableBonusSpawner;
    }

    private void OnEnable() => MessageBus.Subscribe(this);
    private void OnDisable () => MessageBus.Unsubscribe(this);


    public void SpawnBonusObject(BonusId bonusId, Vector3 position)
    {
        _droppableBonusSpawner.Spawn(bonusId, position, bonusesContainer);
    }

    public void ReturnToPool(DroppableBonus droppableBonus)
    {
        _droppableBonusSpawner.Destroy(droppableBonus);
    }

    public void OnClearGameField()
    {
        _droppableBonusSpawner.ClearAll();
    }
}
