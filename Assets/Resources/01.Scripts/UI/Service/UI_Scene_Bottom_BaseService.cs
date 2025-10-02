using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Scene_Bottom_BaseService : MonoBehaviour
{
    [FindComponents("View"), SerializeField] private UI_Scene_Bottom_BaseView view;
    [FindComponents("Model"), SerializeField] private UI_Scene_Bottom_BaseModel model;

    public void Init()
    {
        InjectUtil.InjectComponents(this);

        view.Init();
        model.Init();
    }

    public void Release()
    {
        view.Release();
        model.Release();
    }
}
