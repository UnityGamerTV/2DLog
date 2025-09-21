using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : Singleton<EventManager>, IManager
{
    private Dictionary<EVENT_TYPE, List<IListener>> listeners = new();

    public void Init()
    {

    }

    public void AddListener(EVENT_TYPE eventType, IListener listener)
    {
        List<IListener> listenList = null;

        if(listeners.TryGetValue(eventType, out listenList))
        {
            listenList.Add(listener);
            return;
        }

        listenList = new List<IListener>();
        listenList.Add(listener);
        listeners.Add(eventType, listenList);
    }

    public void PostNotification(EVENT_TYPE eventType, Component sender, object param = null)
    {
        List<IListener> listenList = null;
        if (!listeners.TryGetValue(eventType, out listenList))
            return;

        for (int i = 0; i < listenList.Count; i++)
        {
            if (!listenList[i].Equals(null))
                listenList[i].OnEvent(eventType, sender, param);
        }
    }

    public void RemoveEvent(EVENT_TYPE eventType)
    {
        listeners.Remove(eventType);
    }

    public void RemoveRedundancies()
    {
        Dictionary<EVENT_TYPE, List<IListener>> tempListeners = new();

        foreach (KeyValuePair<EVENT_TYPE, List<IListener>> item in listeners)
        {
            for (int i = item.Value.Count -1; i>= 0; i++)
            {
                if (item.Value[i].Equals(null))
                    item.Value.RemoveAt(i);
            }

            if (item.Value.Count > 0)
                tempListeners.Add(item.Key, item.Value);
        }
        listeners = tempListeners;
    }

    public void Release()
    {
        
    }
}
