using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupsManager : MonoBehaviour
{
    private PopupsContainer _container;
    private Stack<BasePopup> _stackOfPopups;
    
    public void Init(PopupsContainer container)
    {
        _stackOfPopups = new Stack<BasePopup>();
        _container = container;
    }

    public IEnumerator Show<T>(Action onComplete = null) where T : BasePopup
    {
        var popup = GetPopup<T>();
        popup.transform.SetAsLastSibling();
        _stackOfPopups.Push(popup);
        yield return popup.Show(onComplete);
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
    
    public IEnumerator HideLast()
    {
        var last = _stackOfPopups.Pop();
        yield return last.Hide();
        last.gameObject.SetActive(false);
    }

    public IEnumerator HideAll()
    {
        for (int i = 0; i < _stackOfPopups.Count; i++)
        {
            var popup = _stackOfPopups.Peek();
            yield return popup.Hide();
        }
        _stackOfPopups.Clear();
    }
}
