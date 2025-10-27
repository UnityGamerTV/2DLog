using System;
using UnityEngine;

public class UI_Popup_StatusModel : MonoBehaviour
{
    public ItemDataComponent _helmet { get => helmet; set { helmet = value; helmetUpdateAction.Invoke(helmet); } }
    [SerializeField] private ItemDataComponent helmet;
    public ItemDataComponent _armor { get => armor; set { armor = value; armorUpdateAction.Invoke(armor); } }
    [SerializeField] private ItemDataComponent armor;
    public ItemDataComponent _amulet { get => amulet; set { amulet = value; amuletUpdateAction.Invoke(amulet); } }
    [SerializeField] private ItemDataComponent amulet;
    public ItemDataComponent _weapon { get => weapon; set { weapon = value; weaponUpdateAction.Invoke(weapon); } }
    [SerializeField] private ItemDataComponent weapon;
    public ItemDataComponent _shield { get => shield; set { shield = value; shieldUpdateAction.Invoke(weapon); } }
    [SerializeField] private ItemDataComponent shield;
    public ItemDataComponent _ring1 { get => ring1; set { ring1 = value; ring1UpdateAction.Invoke(ring1); } }
    [SerializeField] private ItemDataComponent ring1;
    public ItemDataComponent _ring2 { get => ring2; set { ring2 = value; ring2UpdateAction.Invoke(ring2); } }
    [SerializeField] private ItemDataComponent ring2;

    public Action<ItemDataComponent> helmetUpdateAction;
    public Action<ItemDataComponent> armorUpdateAction;
    public Action<ItemDataComponent> amuletUpdateAction;
    public Action<ItemDataComponent> weaponUpdateAction;
    public Action<ItemDataComponent> shieldUpdateAction;
    public Action<ItemDataComponent> ring1UpdateAction;
    public Action<ItemDataComponent> ring2UpdateAction;

    public void Init()
    {

    }
}
