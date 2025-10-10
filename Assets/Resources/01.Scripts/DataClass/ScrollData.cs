using System;

[Serializable]
public class ScrollData : INickname
{
    public int _id { get; set; }
    public string _name { get; set; }
    public int? _evasion { get; set; }
    public int? _enhance_value { get; set; }
    public DataType _data_type { get; set; }
    public InventoryType _inventory_type { get; set; }
    public ItemType _item_type { get; set; }
    public int? _turn { get; set; }
    public string _nickname { get; set; }
    public string _comment { get; set; }
}