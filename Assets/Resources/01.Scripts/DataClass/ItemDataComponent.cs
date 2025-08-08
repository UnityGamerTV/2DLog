using UnityEngine;

public class BaseDataComponent : MonoBehaviour { }

public class ItemDataComponent : BaseDataComponent
{
    public int _no { get { return no; } set { no = value; } }
    [SerializeField] private int no;
    public string _name { get { return itemName; } set { itemName = value; } }
    [SerializeField] private string itemName;
    public int? _max_hp { get { return maxHp; } set { maxHp = CheckNullValue(value); } }
    [SerializeField] private int maxHp;
    public int? _max_mp { get { return maxMp; } set { maxMp = CheckNullValue(value); } }
    [SerializeField] private int maxMp;
    public int? _min_attack { get { return minAttack; } set { minAttack = CheckNullValue(value); } }
    [SerializeField] private int minAttack;
    public int? _max_attack { get { return maxAttack; } set { maxAttack = CheckNullValue(value); } }
    [SerializeField] private int maxAttack;
    public int? _defence { get { return defence; } set { defence = CheckNullValue(value); } }
    [SerializeField] private int defence;
    public int? _min_magic_attack { get { return minMagicAttack; } set { minMagicAttack = CheckNullValue(value); } }
    [SerializeField] private int minMagicAttack;
    public int? _max_magic_attack { get { return maxMagicAttack; } set { maxMagicAttack = CheckNullValue(value); } }
    [SerializeField] private int maxMagicAttack;
    public int? _fire_res { get { return fireRes; } set { fireRes = CheckNullValue(value); } }
    [SerializeField] private int fireRes;
    public int? _cold_res { get { return coldRes; } set { coldRes = CheckNullValue(value); } }
    [SerializeField] private int coldRes;
    public int? _earth_res { get { return earthRes; } set { earthRes = CheckNullValue(value); } }
    [SerializeField] private int earthRes;
    public int? _dark_res { get { return dartRes; } set { dartRes = CheckNullValue(value); } }
    [SerializeField] private int dartRes;
    public int? _poison_res { get { return poisonRes; } set { poisonRes = CheckNullValue(value); } }
    [SerializeField] private int poisonRes;
    public int? _avoid { get { return avoid; } set { avoid = CheckNullValue(value); } }
    [SerializeField] private int avoid;
    public ItemPropertyType? _item_property_type { get { return itemPropertyType; } set { itemPropertyType = CheckItemPropertyType(value); } }
    [SerializeField] private ItemPropertyType itemPropertyType;
    public int? _hand { get { return hand; } set { hand = CheckNullValue(value); } }
    [SerializeField] private int hand;
    public int? _enhance_limit { get { return enhanceLimit; } set { enhanceLimit = CheckNullValue(value); } }
    [SerializeField] private int enhanceLimit;
    public int? _skill_limit { get { return skillLimit; } set { skillLimit = CheckNullValue(value); } }
    [SerializeField] private int skillLimit;
    public string _nickName { get { return nickName; } set { nickName = value; } }
    [SerializeField] private string nickName;
    public string _comment { get { return comment; } set { comment = value; } }
    [SerializeField] private string comment;

    T CheckNullValue<T>(T? data) where T : struct
    {
        return data ?? default;
    }

    ItemPropertyType CheckItemPropertyType(ItemPropertyType? data)
    {
        return data ?? ItemPropertyType.NONE;
    }
}

