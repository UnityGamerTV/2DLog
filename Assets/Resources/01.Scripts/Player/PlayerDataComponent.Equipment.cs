using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ¿Â¬¯ µ•¿Ã≈Õ
public partial class PlayerDataComponent : DecoratorDataComponent
{
    public Dictionary<EquipmentType, ItemDataComponent> _helmet { get { return helmet; } set { helmet = value; } }
    [SerializeField] private Dictionary<EquipmentType, ItemDataComponent> helmet;
    public Dictionary<EquipmentType, ItemDataComponent> _armour { get { return armour; } set { armour = value; } }
    [SerializeField] private Dictionary<EquipmentType, ItemDataComponent> armour;
    public Dictionary<EquipmentType, ItemDataComponent> _amulet { get { return amulet; } set { amulet = value; } }
    [SerializeField] private Dictionary<EquipmentType, ItemDataComponent> amulet;
    public Dictionary<EquipmentType, ItemDataComponent> _weapon { get { return weapon; } set { weapon = value; } }
    [SerializeField] private Dictionary<EquipmentType, ItemDataComponent> weapon;
    public Dictionary<EquipmentType, ItemDataComponent> _shield { get { return shield; } set { shield = value; } }
    [SerializeField] private Dictionary<EquipmentType, ItemDataComponent> shield;
    public Dictionary<EquipmentType, ItemDataComponent> _ring1 { get { return ring1; } set { ring1 = value; } }
    [SerializeField] private Dictionary<EquipmentType, ItemDataComponent> ring1;
    public Dictionary<EquipmentType, ItemDataComponent> _ring2 { get { return ring2; } set { ring2 = value; } }
    [SerializeField] private Dictionary<EquipmentType, ItemDataComponent> ring2;
}
