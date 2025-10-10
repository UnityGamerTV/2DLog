using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Scene_Equip_InvenView : MonoBehaviour
{
    [Singleton(typeof(ResourceManager))] private ResourceManager resourceManager;

    [FindComponents("Model"), SerializeField] private UI_Scene_Equip_InvenModel model;
    [FindComponents("Slot1", "Slot2", "Slot3", "Slot4", "Slot5", "Slot6", "Slot7", "Slot8", "Slot9", "Slot10," +
        "Slot11", "Slot12", "Slot13", "Slot14", "Slot15")]
    [SerializeField] private List<UI_Scene_Equip_Inven_Slot> slots = new ();
    [FindComponents("ReadingGlassButton"), SerializeField] private Button ReadingGlassButton;

    public void Init()
    {
        InjectUtil.InjectSingleton(this);
        InjectUtil.InjectComponents(this);
    }

    public void Release()
    {

    }
}
