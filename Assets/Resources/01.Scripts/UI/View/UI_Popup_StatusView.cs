using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Popup_StatusView : MonoBehaviour
{
    [FindComponents("EquipmentBG"), SerializeField] private Transform equipmentBG;
    [FindComponents("ResistanceBG"), SerializeField] private Transform resistanceBG;
    [FindComponents("StatusToggle"), SerializeField] private Toggle statusToggle;
    [FindComponents("ResistToggle"), SerializeField] private Toggle resistToggle;
    [FindComponents("Model"), SerializeField] private UI_Popup_StatusModel model;
    [FindComponents("HelmetSlot"), SerializeField] private UI_Popup_Status_Slot helmetSlot;
    [FindComponents("ArmorSlot"), SerializeField] private UI_Popup_Status_Slot armorSlot;
    [FindComponents("AmuletSlot"), SerializeField] private UI_Popup_Status_Slot amuletSlot;
    [FindComponents("WeaponSlot"), SerializeField] private UI_Popup_Status_Slot weaponSlot;
    [FindComponents("ShieldSlot"), SerializeField] private UI_Popup_Status_Slot shieldSlot;
    [FindComponents("RingSlot1"), SerializeField] private UI_Popup_Status_Slot ringSlot1;
    [FindComponents("RingSlot2"), SerializeField] private UI_Popup_Status_Slot ringSlot2;
    [SerializeField] private List<UI_Popup_Status_Slot> slots;

    public void Init()
    {
        InjectUtil.InjectSingleton(this);
        InjectUtil.InjectComponents(this);

        InitSlot();

        statusToggle.onValueChanged.AddListener(OnStatusToggle);
        resistToggle.onValueChanged.AddListener(OnResistToggle);

        model.helmetUpdateAction += UpdateHelmet;
        model.armorUpdateAction += UpdateArmor;
        model.amuletUpdateAction += UpdateAmulet;
        model.weaponUpdateAction += UpdateWeapon;
        model.shieldUpdateAction += UpdateShield;
        model.ring1UpdateAction += UpdateRing1;
        model.ring2UpdateAction += UpdateRing2;
    }

    private void InitSlot()
    {
        slots = new();
        slots.Add(helmetSlot);
        slots.Add(armorSlot);
        slots.Add(amuletSlot);
        slots.Add(weaponSlot);
        slots.Add(shieldSlot);
        slots.Add(ringSlot1);
        slots.Add(ringSlot2);

        for (int i = 0; i < slots.Count; i++)
            slots[i].Init();
    }

    public void OnStatusToggle(bool isOn) => equipmentBG.gameObject.SetActive(isOn);
    public void OnResistToggle(bool isOn) => resistanceBG.gameObject.SetActive(isOn);
    public void UpdateHelmet(ItemDataComponent component) => helmetSlot.SetData(component);
    public void UpdateArmor(ItemDataComponent component) => armorSlot.SetData(component);
    public void UpdateAmulet(ItemDataComponent component) => amuletSlot.SetData(component);
    public void UpdateWeapon(ItemDataComponent component) => weaponSlot.SetData(component);
    public void UpdateShield(ItemDataComponent component) => shieldSlot.SetData(component);
    public void UpdateRing1(ItemDataComponent component) => ringSlot1.SetData(component);
    public void UpdateRing2(ItemDataComponent component) => ringSlot2.SetData(component);

    public void Release()
    {
        model.helmetUpdateAction -= UpdateHelmet;
        model.armorUpdateAction -= UpdateArmor;
        model.amuletUpdateAction -= UpdateAmulet;
        model.weaponUpdateAction -= UpdateWeapon;
        model.shieldUpdateAction -= UpdateShield;
        model.ring1UpdateAction -= UpdateRing1;
        model.ring2UpdateAction -= UpdateRing2;
    }
}
