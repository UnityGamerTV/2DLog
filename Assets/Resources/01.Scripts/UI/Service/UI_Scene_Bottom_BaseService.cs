using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Scene_Bottom_BaseService : MonoBehaviour
{
    [Singleton(typeof(UIManager))] private UIManager uiManager;
    [FindComponents("View"), SerializeField] private UI_Scene_Bottom_BaseView view;
    [FindComponents("Model"), SerializeField] private UI_Scene_Bottom_BaseModel model;

    public void Init()
    {
        InjectUtil.InjectComponents(this);
        InjectUtil.InjectSingleton(this);

        view.Init();
        model.Init();
    }

    public void OnClickStatusButton() => uiManager.ShowPopupUI<UI_Popup_StatusController>(UI_POPUP_ENUM.UI_Popup_Status);

    public void SetUiSceneEnum(UI_SCENE_ENUM uiSceneEnum) => model._uiSceneEnum = uiSceneEnum;

    public UI_SCENE_ENUM GetUISceneEnum() => model._uiSceneEnum;

    public void Release()
    {
        view.Release();
        model.Release();
    }
}
