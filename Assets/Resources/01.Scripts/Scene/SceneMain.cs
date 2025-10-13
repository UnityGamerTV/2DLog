using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneMain : MonoBehaviour, IListener
{
    [Singleton(typeof(EventManager))] private EventManager eventManager;
    [Singleton(typeof(MapManager))] private MapManager mapManager;
    [Singleton(typeof(UIManager))] private UIManager uiManager;

    [SerializeField] private UI_Scene_Bottom_BaseController bottomBaseController;


    public void Start()
    {
        InjectUtil.InjectSingleton(this);

        mapManager.GenerateMap("Base2", "Map_001");

        eventManager.AddListener(EVENT_BOTTOM_BASE_UI.ON_CLICK_DIR_BUTTON, this);
        eventManager.AddListener(EVENT_BOTTOM_BASE_UI.ON_CLICK_CONSUME_BUTTON, this);
        eventManager.AddListener(EVENT_BOTTOM_BASE_UI.ON_CLICK_ABILITY_BUTTON, this);
        eventManager.AddListener(EVENT_BOTTOM_BASE_UI.ON_CLICK_EQUIP_BUTTON, this);
        eventManager.AddListener(EVENT_BOTTOM_BASE_UI.ON_CLICK_SETTING_BUTTON, this);

        // 기본 씬 UI 생성
        bottomBaseController = uiManager.ShowSceneUI<UI_Scene_Bottom_BaseController>(UI_SCENE_ENUM.UI_Scene_Bottom_Base);
        uiManager.ShowSceneUI<UI_Scene_Bottom_DirController>(UI_SCENE_ENUM.UI_Scene_Bottom_Dir);
        bottomBaseController.SetUISceneEnum(UI_SCENE_ENUM.UI_Scene_Bottom_Dir); // Bottom UI 변경 시 사용
        uiManager.ShowSceneUI<UI_Scene_Top_CurrencyController>(UI_SCENE_ENUM.UI_Scene_Top_Currency);
        uiManager.ShowSceneUI<UI_Scene_Top_VitalController>(UI_SCENE_ENUM.UI_Scene_Top_Vital);
    }

    // 이벤트가 과도하게 커지면 따로 관리
    public void OnEvent<TEnum>(TEnum eventType, Component sender, object param = null) where TEnum : Enum
    {
        UI_SCENE_ENUM uiSceneEnum;
        switch (eventType)
        {
            case EVENT_BOTTOM_BASE_UI.ON_CLICK_DIR_BUTTON:
                uiSceneEnum = bottomBaseController.GetUISceneEnum();
                uiManager.CloseSceneUI(uiSceneEnum);
                uiManager.ShowSceneUI<UI_Scene_Bottom_DirController>(UI_SCENE_ENUM.UI_Scene_Bottom_Dir);
                bottomBaseController.SetUISceneEnum(UI_SCENE_ENUM.UI_Scene_Bottom_Dir); // Bottom UI 변경 시 사용
                break;
            case EVENT_BOTTOM_BASE_UI.ON_CLICK_CONSUME_BUTTON:
                uiSceneEnum = bottomBaseController.GetUISceneEnum();
                uiManager.CloseSceneUI(uiSceneEnum);
                uiManager.ShowSceneUI<UI_Scene_Consume_InvenController>(UI_SCENE_ENUM.UI_Scene_Consume_Inven);
                bottomBaseController.SetUISceneEnum(UI_SCENE_ENUM.UI_Scene_Consume_Inven);
                break;
            case EVENT_BOTTOM_BASE_UI.ON_CLICK_ABILITY_BUTTON:
                uiSceneEnum = bottomBaseController.GetUISceneEnum();
                uiManager.CloseSceneUI(uiSceneEnum);
                uiManager.ShowSceneUI<UI_Scene_Ability_InvenController>(UI_SCENE_ENUM.UI_Scene_Ability_Inven);
                bottomBaseController.SetUISceneEnum(UI_SCENE_ENUM.UI_Scene_Ability_Inven);
                break;
            case EVENT_BOTTOM_BASE_UI.ON_CLICK_EQUIP_BUTTON:
                uiSceneEnum = bottomBaseController.GetUISceneEnum();
                uiManager.CloseSceneUI(uiSceneEnum);
                uiManager.ShowSceneUI<UI_Scene_Equip_InvenController>(UI_SCENE_ENUM.UI_Scene_Equip_Inven);
                bottomBaseController.SetUISceneEnum(UI_SCENE_ENUM.UI_Scene_Equip_Inven);
                break;
            case EVENT_BOTTOM_BASE_UI.ON_CLICK_SETTING_BUTTON: // 옵션UI 는 아직 제작하지 않음
                break;

        }
    }
}
