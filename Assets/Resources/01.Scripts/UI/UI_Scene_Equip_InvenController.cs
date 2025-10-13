using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Scene_Equip_InvenController : UI_Scene, IListener
{
    [Singleton(typeof(EventManager))] private EventManager eventManager;

    [FindComponents("Service"), SerializeField] private UI_Scene_Equip_InvenService service;
    [FindComponents("ReadingGlassButton"), SerializeField] private UnityEngine.UI.Button button;

    public override void Init()
    {
        base.Init();

        InjectUtil.InjectComponents(this);
        InjectUtil.InjectSingleton(this);

        button.onClick.AddListener(OnClickReadingGlassButton);

        eventManager.AddListener(EVENT_PLAYER.PLAYER_EQUIP_INVENTORY_UPDATED, this);

        service.Init();
    }

    public void OnClickReadingGlassButton() => service.OnClickReadingGlassButton();

    public void OnEvent<TEnum>(TEnum eventType, Component sender, object param = null) where TEnum : Enum
    {
        switch (eventType)
        {
            case EVENT_PLAYER.PLAYER_EQUIP_INVENTORY_UPDATED: service.UpdateData(param); break;
        }
    }

    public override void Release()
    {
        base.Release();

        button.onClick.RemoveListener(OnClickReadingGlassButton);

        service.Release();
    }
}
