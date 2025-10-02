using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Scene_Ability_InvenService : MonoBehaviour
{
    [FindComponents("Model"), SerializeField] private UI_Scene_Ability_InvenModel model;
    [FindComponents("View"), SerializeField] private UI_Scene_Ability_InvenView view;

    public void Init()
    {
        InjectUtil.InjectComponents(this);

        model.Init();
        view.Init();
    }

    public void Release()
    {
        model.Release();
        view.Release();
    }
}
