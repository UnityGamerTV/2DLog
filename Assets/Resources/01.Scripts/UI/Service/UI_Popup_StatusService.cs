using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Popup_StatusService : MonoBehaviour
{
    [FindComponents("View"), SerializeField] private UI_Popup_StatusView view;
    [FindComponents("Model"), SerializeField] private UI_Popup_StatusModel model;

    public void Init()
    {
        InjectUtil.InjectComponents(this);

        model.Init();
        view.Init();
    }

    public void UpdateEquip(List<ItemDataComponent> itemDataComponents)
    {
        for (int i = 0; i < itemDataComponents.Count; i++)
        {
            var component = itemDataComponents[i];
            if (component == null || component._data == null)
                continue;

            var slot = itemDataComponents[i]._data._equip_slot;
            if (!slot.HasValue)
                continue;

            switch(slot)
            {
                case EquipSlot.HELMET: model._helmet = itemDataComponents[i]; break;
                case EquipSlot.ARMOR: model._armor = itemDataComponents[i]; break;
                case EquipSlot.AMULET: model._amulet = itemDataComponents[i]; break;
                case EquipSlot.WEAPON: model._weapon = itemDataComponents[i]; break;
                case EquipSlot.SHIELD: model._shield = itemDataComponents[i]; break;
                case EquipSlot.RING1: model._ring1 = itemDataComponents[i]; break;
                case EquipSlot.RING2: model._ring2 = itemDataComponents[i]; break;
            }
        }
    }
}
