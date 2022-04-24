using System;
using System.Collections.Generic;
using System.Linq;

public static class MessageBus
{
    private static readonly Dictionary<Type, SubscribersList<ISubscriber>> Subscribers = new Dictionary<Type, SubscribersList<ISubscriber>>();
    private static readonly Dictionary<Type, List<Type>> CachedSubscriberTypes = new Dictionary<Type, List<Type>>();

    public static void Subscribe(ISubscriber subscriber)
    {
        List<Type> subscriberTypes = GetSubscriberTypes(subscriber);
        foreach (Type t in subscriberTypes)
        {
            if (!Subscribers.ContainsKey(t))
            {
                Subscribers[t] = new SubscribersList<ISubscriber>();
            }
            Subscribers[t].Add(subscriber);
        }
    }

    public static void Unsubscribe(ISubscriber subscriber)
    {
        List<Type> subscriberTypes = GetSubscriberTypes(subscriber);
        foreach (Type t in subscriberTypes)
        {
            if (Subscribers.ContainsKey(t))
                Subscribers[t].Remove(subscriber);
        }
    }

    public static void RaiseEvent<T>(Action<T> action) where T : class, ISubscriber
    {
        var type = typeof(T);
            
        if (!Subscribers.ContainsKey(type)) return;
            
        SubscribersList<ISubscriber> subscribersList = Subscribers[type];

        subscribersList.IsExecuting = true;
        foreach (ISubscriber subscriber in subscribersList.List)
        {
            action.Invoke(subscriber as T);
        }
        subscribersList.IsExecuting = false;
        subscribersList.ClearNullSubs();
    }
    
    private static List<Type> GetSubscriberTypes(ISubscriber globalSubscriber)
    {
        Type type = globalSubscriber.GetType();
        if (CachedSubscriberTypes.ContainsKey(type))
        {
            return CachedSubscriberTypes[type];
        }
        List<Type> subscriberTypes = GetListOfSubTypes(type);
        CachedSubscriberTypes[type] = subscriberTypes;
        
        return subscriberTypes;
    }
    
    private static List<Type> GetListOfSubTypes(Type type)
    {
        List<Type> subscriberTypes = new List<Type>();
        foreach (var t in type.GetInterfaces())
        {
            if (t.GetInterfaces().Contains(typeof(ISubscriber)))
            {
                subscriberTypes.Add(t);
            }
        }
        return subscriberTypes;
    }
}
