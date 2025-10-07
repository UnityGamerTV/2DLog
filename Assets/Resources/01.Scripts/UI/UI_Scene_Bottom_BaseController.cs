using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Scene_Bottom_BaseController : UI_Scene
{
    [FindComponents("Service"), SerializeField] private UI_Scene_Bottom_BaseService service;
    public override void Init()
    {
        base.Init();
        InjectUtil.InjectComponents(this);
        service.Init();
    }

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
    }
}
