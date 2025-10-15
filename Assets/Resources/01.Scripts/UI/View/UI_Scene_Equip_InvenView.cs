using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Scene_Equip_InvenView : MonoBehaviour
{
    [Singleton(typeof(ResourceManager))] private ResourceManager resourceManager;

    [FindComponents("Model"), SerializeField] private UI_Scene_Equip_InvenModel model;
    [FindComponents("Slot1", "Slot2", "Slot3", "Slot4", "Slot5", "Slot6", "Slot7", "Slot8", "Slot9", "Slot10", "Slot11", "Slot12", "Slot13", "Slot14", "Slot15")]
    [SerializeField] private List<UI_Scene_Equip_Inven_Slot> slots = new();
    [FindComponents("ReadingGlassButton"), SerializeField] private Button ReadingGlassButton;

    public void Init()
    {
        //slots = new();

        InjectUtil.InjectSingleton(this);
        InjectUtil.InjectComponents(this);

        model.updateAction += UpdateData;

        for (int i = 0; i < slots.Count; i++)
        {
            slots[i].Init();
        }
    }

    public void OnClickReadingGlassButton(bool onDetail)
    {
        if (onDetail)
            for (int i = 0; i < slots.Count; i++)
                slots[i].OnDetail();
        else
            for (int i = 0; i < slots.Count; i++)
                slots[i].OffDetail();
    }

    public void UpdateData(List<ItemDataComponent> data)
    {
        for (int i = 0; i < data.Count; i++)
            slots[i]._itemDataComponent = data[i];
    }

    public void Release()
    {
        model.updateAction -= UpdateData;
    }
}
