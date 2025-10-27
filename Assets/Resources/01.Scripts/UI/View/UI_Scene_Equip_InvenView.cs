using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Scene_Equip_InvenView : MonoBehaviour
{
    [Singleton(typeof(ResourceManager))] private ResourceManager resourceManager;
    [Singleton(typeof(EventManager))] private EventManager eventManager;

    [FindComponents("Model"), SerializeField] private UI_Scene_Equip_InvenModel model;
    [FindComponents("Slot1", "Slot2", "Slot3", "Slot4", "Slot5", "Slot6", "Slot7", "Slot8", "Slot9", "Slot10", "Slot11", "Slot12", "Slot13", "Slot14", "Slot15")]
    [SerializeField] private List<UI_Scene_Equip_Inven_Slot> slots = new();
    [FindComponents("ReadingGlassButton"), SerializeField] private Button ReadingGlassButton;

    public void Init()
    {
        InjectUtil.InjectSingleton(this);
        InjectUtil.InjectComponents(this);

        model.updateAction += UpdateData;

        InitSlot();
    }

    public void InitSlot()
    {
        for (int i = 0; i < slots.Count; i++)
            slots[i].Init();
    }

    public void UpdataOnDetail(bool onDetail)
    {
        for (int i = 0; i < slots.Count; i++)
            slots[i].OnDetail(onDetail);

        eventManager.PostNotification(EVENT_EQUIP_INVEN_UI.ON_CLICK_READING_GLASS, this, onDetail);
    }

    public void UpdateData(List<ItemDataComponent> data)
    {
        for (int i = data.Count; i < slots.Count; i++)
            slots[i]._itemDataComponent = null;

        for (int i = 0; i < data.Count; i++)
            slots[i]._itemDataComponent = data[i];
    }

    public void Release()
    {
        model.updateAction -= UpdateData;
        eventManager.PostNotification(EVENT_EQUIP_INVEN_UI.ON_CLICK_READING_GLASS, this);
    }
}
