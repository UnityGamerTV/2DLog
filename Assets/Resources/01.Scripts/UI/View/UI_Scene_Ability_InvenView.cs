using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Scene_Ability_InvenView : MonoBehaviour
{
    
    [FindComponents("Model"), SerializeField] private UI_Scene_Ability_InvenModel model;
    [FindComponents("Slot1", "Slot2", "Slot3", "Slot4", "Slot5", "Slot6", "Slot7", "Slot8")]
    [SerializeField] private List<UI_Scene_Ability_Inven_Slot> slots = new ();
    [FindComponents("ReadingGlassButton"), SerializeField] private Button readingGlassButton;

    public void Init()
    {
        InjectUtil.InjectComponents(this);
        SlotInit();

        readingGlassButton.onClick.AddListener(OnButtonClick);
    }
    
    private void SlotInit()
    {
        for (int i = 0; i < slots.Count; i++)
            slots[i].Init();
    }

    public void OnButtonClick()
    {
        model._onDetail = !model._onDetail;
        if (model._onDetail)
            OnDetail();
        else
            OffDetail();
    }

    private void OnDetail()
    {
        for (int i = 0; i < slots.Count; i++)
            slots[i].OnDetail();
    }

    private void OffDetail()
    {
        for (int i = 0; i < slots.Count; i++)
            slots[i].OffDetail();
    }

    public void Release()
    {

    }
}
