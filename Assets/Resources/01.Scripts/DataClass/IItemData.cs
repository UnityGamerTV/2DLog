using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBaseData
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
    public string _nickname { get; set; }
    public string _comment { get; set; }
}
