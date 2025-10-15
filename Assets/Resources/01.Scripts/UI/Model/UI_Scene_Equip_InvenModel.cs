using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Scene_Equip_InvenModel : MonoBehaviour
{
    public bool _onDetail { get { return onDetail; } set { onDetail = value; } }
    [SerializeField] private bool onDetail;

    public List<ItemDataComponent> _equipmentInven { get => equipmentInven;  set { equipmentInven = value; updateAction.Invoke(equipmentInven); } }
    [SerializeField] private List<ItemDataComponent> equipmentInven;

    public Action<List<ItemDataComponent>> updateAction;

    public void Init()
    {
        equipmentInven = new();
    }

    public void UpdateData(object data)
    {
        if (data is List<ItemDataComponent>)
            _equipmentInven = (List<ItemDataComponent>)data;
    }

    public void Release()
    {

    }
}
