using UnityEngine;
using System;

public interface IListener
{
    void OnEvent<TEnum>(TEnum eventType, Component sender, object param = null) where TEnum : Enum;
}
