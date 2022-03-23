using UnityEngine;
using Zenject;

public class BonusesOnSceneController : MonoBehaviour, IBonusLifecycleHandler, IClearGameFieldHandler
{
    [SerializeField] private Transform bonusesContainer;
    private BonusesSpawner bonusesSpawner;
    
    [Inject]
    public void Init(BonusesSpawner bonusesSpawner)
    {
        this.bonusesSpawner = bonusesSpawner;
    }

    private void OnEnable() => MessageBus.Subscribe(this);
    private void OnDisable () => MessageBus.Unsubscribe(this);


    public void SpawnDroppableBonus(BonusId bonusId, Vector3 position)
    {
        bonusesSpawner.SpawnDroppableBonus(bonusId, position, bonusesContainer);
    }

    public void StartIntrablockBonusAction(BonusId bonusId, Vector2 position)
    {
        var reproducer = bonusesSpawner.GetEffectReproducer(bonusId, position);
        reproducer.Reproduce();
    }

    public void ReturnToPool(DroppableBonus droppableBonus)
    {
        bonusesSpawner.Destroy(droppableBonus);
    }

    public void OnClearGameField()
    {
        bonusesSpawner.ClearAll();
    }
}
