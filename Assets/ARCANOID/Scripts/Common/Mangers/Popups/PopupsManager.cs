using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class PopupsManager : MonoBehaviour
{
    [Inject] private PoolsManager _poolsManager;
    private Transform _container;
    private Dictionary<Type, BasePopup> _popupsStorage;
    private Stack<BasePopup> _stackOfPopups;
    
    public void Init(PopupsContainer container)
    {
        _popupsStorage = new Dictionary<Type, BasePopup>();
        _stackOfPopups = new Stack<BasePopup>();
        _container = container.transform;
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
        if (!_popupsStorage.ContainsKey(popupType))
        {
            CreateAndSavePopup<T>(popupType);
        }
        return _popupsStorage[popupType];
    }

    private void CreateAndSavePopup<T>(Type popupType) where T : BasePopup
    {
        var popup = _poolsManager.GetItem<T>(Vector3.zero, _container);
        var popupRect = (RectTransform) popup.transform;
        popupRect.RefreshScaleAndPosition();
        _popupsStorage.Add(popupType, popup);
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

    public void ClearAll()
    {
        foreach (var entry in _popupsStorage)
        {
            _poolsManager.ReturnItemToPool(entry.Key, entry.Value);
        }
        _popupsStorage.Clear();
        _stackOfPopups.Clear();
    }
}
