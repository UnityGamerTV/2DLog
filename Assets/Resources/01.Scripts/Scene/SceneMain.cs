using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneMain : MonoBehaviour, IListener
{
    [Singleton(typeof(EventManager)), SerializeField] private EventManager eventManager;
    [Singleton(typeof(UIManager))] private UIManager uiManager;

    public void Start()
    {
        InjectUtil.InjectSingleton(this);

        eventManager.AddListener(EVENT_BOTTOM_BASE_UI.ON_CLICK_DIR_BUTTON, this);
        eventManager.AddListener(EVENT_BOTTOM_BASE_UI.ON_CLICK_CONSUME_BUTTON, this);
        eventManager.AddListener(EVENT_BOTTOM_BASE_UI.ON_CLICK_SKILL_BUTTON, this);
        eventManager.AddListener(EVENT_BOTTOM_BASE_UI.ON_CLICK_EQUIP_BUTTON, this);
        eventManager.AddListener(EVENT_BOTTOM_BASE_UI.ON_CLICK_SETTING_BUTTON, this);
    }

    // 이벤트가 과도하게 커지면 따로 관리
    public void OnEvent<TEnum>(TEnum event_Type, Component sender, object param = null) where TEnum : Enum
    {
        switch (event_Type)
        {
            case EVENT_BOTTOM_BASE_UI.ON_CLICK_DIR_BUTTON: 
                uiManager.ShowSceneUI<UI_Scene_Bottom_DirController>(UI_SCENE_ENUM.UI_Scene_Bottom_Dir); break;
            case EVENT_BOTTOM_BASE_UI.ON_CLICK_CONSUME_BUTTON:
                uiManager.ShowSceneUI<UI_Scene_Consume_InvenController>(UI_SCENE_ENUM.UI_Scene_Consume_Inven); break;
            //case EVENT_BOTTOM_BASE_UI.ON_CLICK_SKILL_BUTTON:
        }
    }
}
