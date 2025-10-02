using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Scene_Bottom_BaseView : MonoBehaviour
{
    [Singleton(typeof(EventManager))] private EventManager eventManager;
    [FindComponents("ControllerButton"), SerializeField] private Button controllerButton;
    [FindComponents("ConsumeButton"), SerializeField] private Button consumeButton;
    [FindComponents("SkillButton"), SerializeField] private Button skillButton;
    [FindComponents("EquipButton"), SerializeField] private Button equipButton;
    [FindComponents("SettingButton"), SerializeField] private Button settingButton;

    public void Init()
    {
        InjectUtil.InjectSingleton(this);
        InjectUtil.InjectComponents(this);

        controllerButton.onClick.AddListener(OnClickControllerButton);
        consumeButton.onClick.AddListener(OnClickConsumeButton);
        skillButton.onClick.AddListener(OnClickSkillButton);
        equipButton.onClick.AddListener(OnClickEquipButton);
        settingButton.onClick.AddListener(OnClickSettingButton);
    }

    public void OnClickControllerButton() => eventManager.PostNotification(EVENT_BOTTOM_BASE_UI.ON_CLICK_DIR_BUTTON, this);
    public void OnClickConsumeButton() => eventManager.PostNotification(EVENT_BOTTOM_BASE_UI.ON_CLICK_CONSUME_BUTTON, this);
    public void OnClickSkillButton() => eventManager.PostNotification(EVENT_BOTTOM_BASE_UI.ON_CLICK_SKILL_BUTTON, this);
    public void OnClickEquipButton() => eventManager.PostNotification(EVENT_BOTTOM_BASE_UI.ON_CLICK_EQUIP_BUTTON, this);
    public void OnClickSettingButton() => eventManager.PostNotification(EVENT_BOTTOM_BASE_UI.ON_CLICK_SETTING_BUTTON, this);

    public void Release()
    {
        controllerButton.onClick.RemoveListener(OnClickControllerButton);
        consumeButton.onClick.RemoveListener(OnClickConsumeButton);
        skillButton.onClick.RemoveListener(OnClickSkillButton);
        equipButton.onClick.RemoveListener(OnClickEquipButton);
        settingButton.onClick.RemoveListener(OnClickSettingButton);
    }
}
