using UnityEngine;
using static UnityEditor.LightingExplorerTableColumn;

public class BaseDataComponent : MonoBehaviour { }

public class ItemDataComponent : DecoratorDataComponent
{
    protected DecoratorDataComponent decoData;

    public DecoratorDataComponent Set(DecoratorDataComponent decoData)
    {
        this.decoData = decoData;
        return this;
    }

    public int _id { get { return id; } set { id = value; } }
    [SerializeField] private int id;
    public string _name { get { return itemName; } set { itemName = value; } }
    [SerializeField] private string itemName;
    public DataType? _dataType { get { return dataType; } set { dataType = CheckDataType(value); } }
    [SerializeField] private DataType dataType;
    public InventoryType? _inventoryType { get { return inventoryType; } set { inventoryType = CheckInventoryType(value); } }
    [SerializeField] private InventoryType inventoryType;
    public ItemType? _item_type { get { return itemType; } set { itemType = CheckItemType(value); } }
    [SerializeField] private ItemType itemType;
    public SlotType? _slot_type { get { return slotType; } set { slotType = CheckSlotType(value); } }
    [SerializeField] private SlotType slotType;
    public ElementType? _element_type { get { return elementType; } set { elementType = CheckElementType(value); } }
    [SerializeField] private ElementType elementType;
    public int? _hand_type { get { return handType; } set { handType = CheckNullValue(value); } }
    [SerializeField] private int handType;
    public int? _current_enhance { get { return currentEnhance; } set { currentEnhance = CheckNullValue(value); } }
    [SerializeField] private int currentEnhance;
    public int? _max_enhance { get { return maxEnhance; } set { maxEnhance = CheckNullValue(value); } }
    [SerializeField] private int maxEnhance;
    public int? _required_skill_level { get { return requiredSkillLevel; } set { requiredSkillLevel = CheckNullValue(value); } }
    [SerializeField] private int requiredSkillLevel;
    public string _nickname { get { return nickname; } set { nickname = value; } }
    [SerializeField] private string nickname;
    public string _comment { get { return comment; } set { comment = value; } }
    [SerializeField] private string comment;


    public override void Operation()
    {
        decoData.Operation();
        decoData._current_hp += currentHp;
        decoData._current_mp += currentMp;
        decoData._max_hp += maxHp;
        decoData._max_mp += maxMp;
        decoData._min_attack += minAttack;
        decoData._max_attack += maxAttack;
        decoData._defense += defense;
        decoData._min_magic_attack += minMagicAttack;
        decoData._max_magic_attack += minMagicAttack;
        decoData._fire_resist += fireResist;
        decoData._cold_resist += coldResist;
        decoData._earth_resist += earthResist;
        decoData._dark_resist += darkResist;
        decoData._poison_resist += poisonResist;
        decoData._evasion += evasion;
    }

    public override void Revert()
    {
        decoData.Revert();
        decoData._current_hp -= currentHp;
        decoData._current_mp -= currentMp;
        decoData._max_hp -= maxHp;
        decoData._max_mp -= maxMp;
        decoData._min_attack -= minAttack;
        decoData._max_attack -= maxAttack;
        decoData._defense -= defense;
        decoData._min_magic_attack -= minMagicAttack;
        decoData._max_magic_attack -= minMagicAttack;
        decoData._fire_resist -= fireResist;
        decoData._cold_resist -= coldResist;
        decoData._earth_resist -= earthResist;
        decoData._dark_resist -= darkResist;
        decoData._poison_resist -= poisonResist;
        decoData._evasion -= evasion;
    }
    DataType CheckDataType(DataType? data)
    {
        return data ?? DataType.NONE;
    }

    InventoryType CheckInventoryType(InventoryType? data)
    {
        return data ?? InventoryType.NONE;
    }

    ItemType CheckItemType(ItemType? data)
    {
        return data ?? ItemType.NONE;
    }

    SlotType CheckSlotType(SlotType? data)
    {
        return data ?? SlotType.NONE;
    }

    ElementType CheckElementType(ElementType? data)
    {
        return data ?? ElementType.NONE;
    }
}

