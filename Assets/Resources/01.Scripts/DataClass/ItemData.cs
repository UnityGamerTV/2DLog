using System;

[Serializable]
public class ItemData : INickname
{
    public int _id { get; set; }
    public string _name { get; set; }
    public int? _max_hp { get; set; }
    public int? _max_mp { get; set; }
    public int? _min_attack { get; set; }
    public int? _max_attack { get; set; }
    public int? _defense { get; set; }
    public int? _min_magic_attack { get; set; }
    public int? _max_magic_attack { get; set; }
    public int? _fire_resist { get; set; }
    public int? _cold_resist { get; set; }
    public int? _earth_resist { get; set; }
    public int? _dark_resist { get; set; }
    public int? _poison_resist { get; set; }
    public int? _evasion { get; set; }
    public DataType? _data_type { get; set; }
    public InventoryType? _inventory_type { get; set; }
    public ItemType? _Item_type { get; set; }
    public SlotType? _slot_type { get; set; }
    public ElementType? _element_type { get; set; }
    public int? _hand_type { get; set; }
    public int? _current_enhance { get; set;}
    public int? _max_enhance { get; set; }
    public int? _required_skill_level { get; set; }
    public string _nickname { get; set; }
    public string _comment { get; set; }    
}

public enum DataType
{
    ITEMDATA,
    POTIONDATA,
    SCROLLDATA,
    MAGICDATA,
    MELEEDATA,
    ETCDATA,
    NONE,
}

public enum InventoryType
{
    EQUIPMENT,
    CONSUME,
    ABILITY,
    NONE,
}

public enum ItemType
{
    AMULET,
    ARMOR,
    AXE,
    BOOTS,
    BOW,
    GLOVE,
    HELMET,
    MACE,
    RING,
    ROBE,
    SHIELD,
    SPEAR,
    STAFF,
    SWORD,
    ETC,
    POTION,
    SCROLL,
    NONE,
}

public enum SlotType
{
    AMULET,
    ARMOR,
    HELMET,
    WEAPON,
    SHIELD,
    RING,
    NONE,
}

public enum ElementType
{
    FIRE,
    COLD,
    EARTH,
    POISON,
    DARK,
    NECRO,
    SUM,
    NONE,
}
