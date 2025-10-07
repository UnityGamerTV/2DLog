using UnityEngine;
public class ScrollDataComponent : DecoratorDataComponent
{
    protected DecoratorDataComponent decoData;

    public DecoratorDataComponent Set(DecoratorDataComponent decoData)
    {
        this.decoData = decoData;
        return this;
    }

    public int _id { get { return id; } set { id = value; } }
    [SerializeField] private int id;
    public string _name { get { return scrollName; } set { scrollName = value; } }
    [SerializeField] private string scrollName;
    public int? _enhance_value { get { return enhanceValue; } set { enhanceValue = CheckNullValue(value); } }
    [SerializeField] private int enhanceValue;
    public DataType? _data_type { get { return dataType; } set { dataType = CheckDataType(value); } }
    [SerializeField] private DataType dataType;
    public InventoryType? _inventory_type { get { return inventoryType; } set { inventoryType = CheckInventoryType(value); } }
    [SerializeField] private InventoryType inventoryType;
    public ItemType? _item_type { get { return itemType; } set { itemType = CheckItemType(value); } }
    [SerializeField] private ItemType itemType;
    public int? _turn { get { return turn; } set { turn = CheckNullValue(value); } }
    [SerializeField] private int turn;
    public string _nickName { get { return nickName; } set { nickName = value; } }
    [SerializeField] private string nickName;
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
}
