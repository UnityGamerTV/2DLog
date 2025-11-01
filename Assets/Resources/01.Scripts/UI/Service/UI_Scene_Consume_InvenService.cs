using System.Collections.Generic;
using UnityEngine;

public class UI_Scene_Consume_InvenService : MonoBehaviour
{
    [FindComponents("Model"), SerializeField] private UI_Scene_Consume_InvenModel model;
    [FindComponents("View"), SerializeField] private UI_Scene_Consume_InvenView view;

    public void Init()
    {
        InjectUtil.InjectComponents(this);

        model.Init();
        view.Init();
    }

    public void OnClickReadingGlassButton()
    {
        model._onDetail = !model._onDetail;
        view.UpdataOnDetail(model._onDetail);
    }

    public void UpdateData(Dictionary<ConsumeType, InvenItemData> data)
    {
        model.UpdateData(data);
        view.UpdataOnDetail(model._onDetail);
    }

    public void ResponseGlassData() => model.ResponseGlassData();

    public void Release()
    {
        model.Release();
        view.Release();
    }
}
