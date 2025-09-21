using UnityEngine;

public interface IListener
{
    void OnEvent(EVENT_TYPE event_Type, Component sender, object param = null);
}
