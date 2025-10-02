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

    public override void Release()
    {
        base.Release();
        service.Release();
    }
}
