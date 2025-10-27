using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Scene_Bottom_BaseController : UI_Scene
{
    [Singleton(typeof(EventManager))] private EventManager eventManager;
    [FindComponents("StatusButton"), SerializeField] private Button statusButton;
    [FindComponents("ControllerButton"), SerializeField] private Button controllerButton;
    [FindComponents("ConsumeButton"), SerializeField] private Button consumeButton;
    [FindComponents("SkillButton"), SerializeField] private Button skillButton;
    [FindComponents("EquipButton"), SerializeField] private Button equipButton;
    [FindComponents("SettingButton"), SerializeField] private Button settingButton;
    [FindComponents("Service"), SerializeField] private UI_Scene_Bottom_BaseService service;

    public override void Init()
    {
        base.Init();

        InjectUtil.InjectSingleton(this);
        InjectUtil.InjectComponents(this);

        statusButton.onClick.AddListener(OnClickStatusButton);
        controllerButton.onClick.AddListener(OnClickControllerButton);
        consumeButton.onClick.AddListener(OnClickConsumeButton);
        skillButton.onClick.AddListener(OnClickSkillButton);
        equipButton.onClick.AddListener(OnClickEquipButton);
        settingButton.onClick.AddListener(OnClickSettingButton);

        service.Init();
    }
    public void OnClickStatusButton() => service.OnClickStatusButton();
    public void OnClickControllerButton() => eventManager.PostNotification(EVENT_BOTTOM_BASE_UI.ON_CLICK_DIR_BUTTON, this);
    public void OnClickConsumeButton() => eventManager.PostNotification(EVENT_BOTTOM_BASE_UI.ON_CLICK_CONSUME_BUTTON, this);
    public void OnClickSkillButton() => eventManager.PostNotification(EVENT_BOTTOM_BASE_UI.ON_CLICK_ABILITY_BUTTON, this);
    public void OnClickEquipButton() => eventManager.PostNotification(EVENT_BOTTOM_BASE_UI.ON_CLICK_EQUIP_BUTTON, this);
    public void OnClickSettingButton() => eventManager.PostNotification(EVENT_BOTTOM_BASE_UI.ON_CLICK_SETTING_BUTTON, this);

    public void SetUISceneEnum(UI_SCENE_ENUM uiSceneEnum)
    {
        if (uiSceneEnum == UI_SCENE_ENUM.UI_Scene_Ability_Inven ||
            uiSceneEnum == UI_SCENE_ENUM.UI_Scene_Bottom_Dir ||
            uiSceneEnum == UI_SCENE_ENUM.UI_Scene_Equip_Inven ||
            uiSceneEnum == UI_SCENE_ENUM.UI_Scene_Consume_Inven)
        {
            service.SetUiSceneEnum(uiSceneEnum);
        }
    }

    public UI_SCENE_ENUM GetUISceneEnum() => service.GetUISceneEnum();

    public override void Release()
    {
        base.Release();

        service.Release();

        statusButton.onClick.RemoveListener(OnClickStatusButton);
        controllerButton.onClick.RemoveListener(OnClickControllerButton);
        consumeButton.onClick.RemoveListener(OnClickConsumeButton);
        skillButton.onClick.RemoveListener(OnClickSkillButton);
        equipButton.onClick.RemoveListener(OnClickEquipButton);
        settingButton.onClick.RemoveListener(OnClickSettingButton);
    }
}
