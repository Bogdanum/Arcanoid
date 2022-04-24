using System.Collections.Generic;

internal class SubscribersList<T> where T : class
{
    private bool _containsNull;

    public bool IsExecuting;

    public readonly List<T> List = new List<T>();

    public void Add(T subscriber)
    {
        List.Add(subscriber);
    }

    public void Remove(T subscriber)
    {
        if (IsExecuting)
        {
            var i = List.IndexOf(subscriber);
            if (i >= 0)
            {
                _containsNull = true;
                List[i] = null;
            }
        }
        else
        {
            List.Remove(subscriber);
        }
    }

    public void ClearNullSubs()
    {
        if (!_containsNull) return;
        
        List.RemoveAll(subscriber => subscriber == null);
        _containsNull = false;
    }
}
