using System;
[Serializable]
public class PotionData : INickname
{
    public int _no { get; set; }
    public string _name { get; set; }
    public int? _max_hp { get; set; }
    public int? _current_hp { get; set; }
    public int? _max_mp { get; set; }
    public int? _current_mp { get; set; }
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
    public int? _turn { get; set; }
    public string _nickName { get; set; }
    public string _comment { get; set; }
}