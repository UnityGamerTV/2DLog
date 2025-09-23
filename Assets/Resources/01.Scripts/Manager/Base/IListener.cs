using UnityEngine;
using System;

public interface IListener
{
    void OnEvent<TEnum>(TEnum event_Type, Component sender, object param = null) where TEnum : Enum;
}
