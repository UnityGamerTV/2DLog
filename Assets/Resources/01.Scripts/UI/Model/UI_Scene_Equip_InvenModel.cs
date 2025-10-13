using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Scene_Equip_InvenModel : MonoBehaviour
{
    public bool _onDetail { get { return onDetail; } set { onDetail = value; } }
    [SerializeField] private bool onDetail;

    public List<InvenItemData> _equipmentInven { get { return equipmentInven; } set { equipmentInven = value; updateAction.Invoke(equipmentInven); } }
    [SerializeField] private List<InvenItemData> equipmentInven;

    public Action<List<InvenItemData>> updateAction;

    public void Init()
    {
        equipmentInven = new();
    }

    public void UpdateData(object data)
    {
        if (data is List<InvenItemData>)
            _equipmentInven = (List<InvenItemData>)data;
    }

    public void Release()
    {

    }
}
