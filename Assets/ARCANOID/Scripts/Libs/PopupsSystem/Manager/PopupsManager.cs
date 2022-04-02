using System;
using System.Collections.Generic;
using UnityEngine;

public class PopupsManager : MonoBehaviour
{
    private PopupsContainer _container;
    private Stack<BasePopup> _stackOfPopups;
    
    public void Init(PopupsContainer container)
    {
        _container = container;
        _stackOfPopups = new Stack<BasePopup>();
    }

    public void Show<T>(Action onComplete = null) where T : BasePopup
    {
        var popup = GetPopup<T>();
        popup.transform.SetAsLastSibling();
        _stackOfPopups.Push(popup);
        popup.Show(onComplete);
    }
    
    private BasePopup GetPopup<T>() where T : BasePopup
    {
        Type popupType = typeof(T);
        if (!_container.PopupsStorage.ContainsKey(popupType))
        {
            Debug.Log("Missing Popup in container!", _container.transform);
        }
        return _container.PopupsStorage[popupType];
    }
    
    public void HideAll()
    {
        for (int i = 0; i < _stackOfPopups.Count; i++)
        {
            HideLast();
        }
        _stackOfPopups.Clear();
    }
    
    public void HideLast()
    {
        if (_stackOfPopups.Count < 1) return;
        var last = _stackOfPopups.Pop();
        last.Hide();
    }

    public void HideAllWithoutAnimation()
    {
        for (int i = 0; i < _stackOfPopups.Count; i++)
        {
            var popup = _stackOfPopups.Pop();
            popup.gameObject.SetActive(false);
        }
        _stackOfPopups.Clear();
    }
}
