using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Scene_Equip_InvenService : MonoBehaviour
{
    [FindComponents("Model"), SerializeField] private UI_Scene_Equip_InvenModel model;
    [FindComponents("View"), SerializeField] private UI_Scene_Equip_InvenView view;

    private bool onDetail;

    public void Init()
    {
        InjectUtil.InjectComponents(this);

        model.Init();
        view.Init();
        onDetail = false;
    }

    public void OnClickReadingGlassButton() 
    {
        onDetail = !onDetail;
        view.OnClickReadingGlassButton(onDetail); 
    }

    public void UpdateData(object data) => model.UpdateData(data);
        

    public void Release()
    {
        model.Release();
        view.Release();
    }
}
