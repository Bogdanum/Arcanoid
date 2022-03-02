using System;
using System.Collections.Generic;
using System.Linq;

public static class MessageBus
{
    private static Dictionary<Type, SubscribersList<ISubscriber>> subscribers = new Dictionary<Type, SubscribersList<ISubscriber>>();
    private static Dictionary<Type, List<Type>> cachedSubscriberTypes = new Dictionary<Type, List<Type>>();

    public static void Subscribe(ISubscriber subscriber)
    {
        List<Type> subscriberTypes = GetSubscriberTypes(subscriber);
        foreach (Type t in subscriberTypes)
        {
            if (!subscribers.ContainsKey(t))
            {
                subscribers[t] = new SubscribersList<ISubscriber>();
            }
            subscribers[t].Add(subscriber);
        }
    }

    public static void Unsubscribe(ISubscriber subscriber)
    {
        List<Type> subscriberTypes = GetSubscriberTypes(subscriber);
        foreach (Type t in subscriberTypes)
        {
            if (subscribers.ContainsKey(t))
                subscribers[t].Remove(subscriber);
        }
    }

    public static void RaiseEvent<T>(Action<T> action) where T : class, ISubscriber
    {
        var type = typeof(T);
            
        if (!subscribers.ContainsKey(type)) return;
            
        SubscribersList<ISubscriber> subscribersList = subscribers[type];

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
        if (cachedSubscriberTypes.ContainsKey(type))
        {
            return cachedSubscriberTypes[type];
        }
        List<Type> subscriberTypes = GetListOfSubTypes(type);
        cachedSubscriberTypes[type] = subscriberTypes;
        
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
