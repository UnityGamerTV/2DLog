using System;

[Serializable]
public class ItemData : INickname
{
    public int _no { get; set; }
    public string _name { get; set; }
    public int? _max_hp { get; set; }
    public int? _max_mp { get; set; }
    public int? _min_attack { get; set; }
    public int? _max_attack { get; set; }
    public int? _defence { get; set; }
    public int? _min_magic_attack { get; set; }
    public int? _max_magic_attack { get; set; }
    public int? _fire_res { get; set; }
    public int? _cold_res { get; set; }
    public int? _earth_res { get; set; }
    public int? _dark_res { get; set; }
    public int? _poison_res { get; set; }
    public int? _avoid { get; set; }
    public ItemPropertyType? _item_property_type { get; set; }
    public int? _hand { get; set; }
    public int? _enhance_limit { get; set; }
    public int? _skill_limit { get; set; }
    public string _nickName { get; set; }
    public string _comment { get; set; }
}

public enum ItemPropertyType
{
    COLD,
    FIRE,
    EARTH,
    POISON,
    DARK,
    NONE,
}
