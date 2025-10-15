using System;
using UnityEngine;

[Serializable]
public class ScrollData : BaseData, INickname //TODO IBaseData를 구현하지 않았음 고민... public int? _evasion 사용하려면 넣어야 되는데 그러면 너무 필요없는 데이터가 많이 들어감
{
    public int _id { get => id; set => id = value; }
    [SerializeField] private int id;
    public string _name { get => itemName; set => itemName = value; }
    [SerializeField] private string itemName;
    public int? _evasion { get => evasion; set => evasion = CheckNullValue(value); }
    [SerializeField] private int evasion;
    public int? _enhance_value { get => enhanceValue; set => enhanceValue = CheckNullValue(value); }
    [SerializeField] private int enhanceValue;
    public ItemGrade _item_grade { get => itemGrade; set => itemGrade = CheckNullValue<ItemGrade>(value); }
    [SerializeField] private ItemGrade itemGrade;
    public DataType _data_type { get => dataType; set => dataType = CheckNullValue<DataType>(value); }
    [SerializeField] private DataType dataType;
    public InventoryType _inventory_type { get => inventoryType; set => inventoryType = CheckNullValue<InventoryType>(value); }
    [SerializeField] private InventoryType inventoryType;
    public ItemType _item_type { get => itemType; set => itemType = CheckNullValue<ItemType>(value); }
    [SerializeField] private ItemType itemType;
    public int? _turn { get => turn; set => turn = CheckNullValue(value); }
    [SerializeField] private int turn;
    public string _nickname { get => nickname; set => nickname = value; }
    [SerializeField] private string nickname;
    public string _comment { get => comment; set => comment = value; }
    [SerializeField] private string comment;
}