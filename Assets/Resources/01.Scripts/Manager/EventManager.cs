using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EventManager : Singleton<EventManager>, IManager
{
    private Dictionary<Type, object> eventTables = new();

    private Dictionary<TEnum, List<IListener>> GetTable<TEnum>() where TEnum : Enum
    {
        var type = typeof(TEnum);
        if (!eventTables.TryGetValue(type, out var table))
        {
            table = new Dictionary<TEnum, List<IListener>>();
            eventTables[type] = table;
        }
        return (Dictionary<TEnum, List<IListener>>)table;
    }

    public void Init()
    {

    }

    public void AddListener<TEnum>(TEnum eventType, IListener listener) where TEnum : Enum
    {
        var table = GetTable<TEnum>();
        if (!table.TryGetValue(eventType, out var list))
        {
            list = new List<IListener>();
            table[eventType] = list;
        }
        list.Add(listener);
    }

    public void PostNotification<TEnum>(TEnum eventType, Component sender, object param = null) where TEnum : Enum
    {
        var table = GetTable<TEnum>();
        if (!table.TryGetValue(eventType, out var list))
            return;

        foreach (var l in list)
            l?.OnEvent(eventType, sender, param);
    }

    public void RemoveEvent<TEnum>(TEnum eventType) where TEnum : Enum
    {
        if (eventTables.TryGetValue(typeof(TEnum), out var obj))
        {
            var table = (Dictionary<TEnum, List<IListener>>)obj;
            table.Remove(eventType);
        }
    }

    public void RemoveRedundancies<TEnum>() where TEnum : Enum
    {
        if (!eventTables.TryGetValue(typeof(TEnum), out var obj)) return;

        var table = (Dictionary<TEnum, List<IListener>>)obj;
        foreach (var kv in table)
        {
            kv.Value.RemoveAll(l => l == null);
        }
    }

    public void Release()
    {
        
    }
}
