using System;
[Serializable]
public class PotionData : INickname
{
    public int _id { get; set; }
    public string _name { get; set; }
    public int? _max_hp { get; set; }
    public int? _current_hp { get; set; }
    public int? _max_mp { get; set; }
    public int? _current_mp { get; set; }
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
    public ItemGrade? _item_grade { get; set; }
    public DataType? _data_type { get; set; }
    public InventoryType? _inventory_type { get; set; }
    public ItemType? _item_type { get; set; }
    public int? _turn { get; set; }
    public string _nickname { get; set; }
    public string _comment { get; set; }
}