using System;
using UnityEngine;

[Serializable]
public class ItemData : BaseData, INickname, IBaseData
{
    public int _id { get => id;  set => id = value; }
    [SerializeField] private int id;
    public string _name { get => itemName;  set => itemName = value; }
    [SerializeField] private string itemName;
    public int? _max_hp { get => maxHp; set => maxHp = CheckNullValue(value); }
    [SerializeField] private int maxHp;
    public int? _max_mp { get => maxMp; set => maxMp = CheckNullValue(value); }
    [SerializeField] private int maxMp;
    public int? _min_attack { get => minAttack; set => minAttack = CheckNullValue(value); }
    [SerializeField] private int minAttack;
    public int? _max_attack { get => maxAttack; set => maxAttack = CheckNullValue(value); }
    [SerializeField] private int maxAttack;
    public int? _defense { get => defense; set => defense = CheckNullValue(value); }
    [SerializeField] private int defense;
    public int? _min_magic_attack { get => minMagicAttack; set => minMagicAttack = CheckNullValue(value); }
    [SerializeField] private int minMagicAttack;
    public int? _max_magic_attack { get => maxMagicAttack; set => maxMagicAttack = CheckNullValue(value); }
    [SerializeField] private int maxMagicAttack;
    public int? _fire_resist { get => fireResist; set => fireResist = CheckNullValue(value); }
    [SerializeField] private int fireResist;
    public int? _cold_resist { get => coldResist; set => coldResist = CheckNullValue(value); }
    [SerializeField] private int coldResist;
    public int? _earth_resist { get => earthResist; set => earthResist = CheckNullValue(value); }
    [SerializeField] private int earthResist;
    public int? _dark_resist { get => darkResist; set => darkResist = CheckNullValue(value); }
    [SerializeField] private int darkResist;
    public int? _poison_resist { get => poisonResist; set => poisonResist = CheckNullValue(value); }
    [SerializeField] private int poisonResist;
    public int? _evasion { get => evasion; set => evasion = CheckNullValue(value); }
    [SerializeField] private int evasion;
    public ItemGrade? _item_grade { get => itemGrade; set => itemGrade = CheckNullValue(value); }
    [SerializeField] private ItemGrade itemGrade;
    public DataType? _data_type { get => dataType; set => dataType = CheckNullValue(value); }
    [SerializeField] private DataType dataType;
    public InventoryType? _inventory_type { get => inventoryType; set => inventoryType = CheckNullValue(value); }
    [SerializeField] private InventoryType inventoryType;
    public ItemType? _item_type { get => itemType; set => itemType = CheckNullValue(value); }
    [SerializeField] private ItemType itemType;
    public EquipSlot? _equip_slot { get => equipSlot; set => equipSlot = CheckNullValue(value); }
    [SerializeField] private EquipSlot equipSlot;
    public ElementType? _element_type { get => elementType; set => elementType = CheckNullValue(value); }
    [SerializeField] private ElementType elementType;
    public int? _hand_type { get => handType; set => handType = CheckNullValue(value); }
    [SerializeField] private int handType;
    public int? _current_enhance { get => currentEnhance; set => currentEnhance = CheckNullValue(value);}
    [SerializeField] private int currentEnhance;
    public int? _max_enhance { get => maxEnhance; set => maxEnhance = CheckNullValue(value); }
    [SerializeField] private int maxEnhance;
    public int? _required_skill_level { get => requiredSkillLevel; set => requiredSkillLevel = CheckNullValue(value); }
    [SerializeField] private int requiredSkillLevel;
    public string _nickname { get => nickName; set => nickName = value; }
    [SerializeField] private string nickName;
    public string _comment { get => comment; set => comment = value; }
    [SerializeField] private string comment;
}

public enum ItemGrade
{ 
    NONE = 0,
    NORMAL,
    RANDART,
    FIXDART,
}

public enum DataType
{
    NONE = 0,
    ITEMDATA,
    POTIONDATA,
    SCROLLDATA,
    MAGICDATA,
    MELEEDATA,
    ETCDATA,
}

public enum InventoryType
{
    NONE = 0,
    EQUIPMENT,
    CONSUME,
    ABILITY,
}

public enum ItemType
{
    NONE = 0,
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
}

public enum EquipSlot
{
    NONE = 0,
    AMULET,
    ARMOR,
    HELMET,
    WEAPON,
    SHIELD,
    RING,
    RING1,
    RING2,
    BOOTS,
    GLOVE,
}

public enum ElementType
{
    NONE = 0,
    FIRE,
    COLD,
    EARTH,
    POISON,
    DARK,
    NECRO,
    SUM,
}
