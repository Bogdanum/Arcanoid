using UnityEngine;
using Zenject;

public class HeaderUIController : MonoBehaviour
{
    [Inject] private PauseController _pauseController;
    
    public void OpenPauseView()
    {
        _pauseController.Pause();
    }
}
