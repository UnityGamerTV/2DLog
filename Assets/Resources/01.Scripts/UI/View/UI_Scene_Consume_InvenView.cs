using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class UI_Scene_Consume_InvenView : MonoBehaviour
{
    [Singleton(typeof(ResourceManager))] private ResourceManager resourceManager;
    [Singleton(typeof(EventManager))] private EventManager eventManager;

    [FindComponents("Model"), SerializeField] private UI_Scene_Consume_InvenModel model;
    [FindComponents("Slot1", "Slot2", "Slot3", "Slot4", "Slot5", "Slot6", "Slot7", "Slot8", "Slot9", "Slot10", "Slot11", "Slot12", "Slot13", "Slot14", "Slot15")]
    [SerializeField] private List<UI_Scene_Consume_Inven_Slot> slots = new();
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

        eventManager.PostNotification(EVENT_CONSUME_INVEN_UI.ON_CLICK_READING_GLASS, this, onDetail);
    }

    public void UpdateData(Dictionary<ConsumeType, InvenItemData> data)
    {
        // 모든 슬롯 초기화
        for (int i = 0; i < slots.Count; i++)
            slots[i]._invenItemData = null;

        // Dictionary의 값들을 슬롯에 할당
        var items = data.Values.ToList();
        for (int i = 0; i < items.Count && i < slots.Count; i++)
            slots[i]._invenItemData = items[i];
    }

    public void Release()
    {
        model.updateAction -= UpdateData;
        eventManager.PostNotification(EVENT_CONSUME_INVEN_UI.ON_CLICK_READING_GLASS, this);
    }
}
