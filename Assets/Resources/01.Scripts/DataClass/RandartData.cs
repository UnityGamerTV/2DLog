using System;

[Serializable]
public class RandartData : BaseData
{
    public int _id { get => id; set => id = value; }
    private int id;
    public string _name { get => itemName; set => itemName = value; }
    private string itemName;
    public int? _max_hp { get => maxHp; set => maxHp = CheckNullValue(value); }
    private int maxHp;
    public int? _max_mp { get => maxMp; set => maxMp = CheckNullValue(value); }
    private int maxMp;
    public int? _min_attack { get => minAttack; set => minAttack = CheckNullValue(value); }
    private int minAttack;
    public int? _max_attack { get => maxAttack; set => maxAttack = CheckNullValue(value); }
    private int maxAttack;
    public int? _defense { get => defense; set => defense = CheckNullValue(value); }
    private int defense;
    public int? _min_magic_attack { get => minMagicAttack; set => minMagicAttack = CheckNullValue(value); }
    private int minMagicAttack;
    public int? _max_magic_attack { get => maxMagicAttack; set => maxMagicAttack = CheckNullValue(value); }
    private int maxMagicAttack;
    public int? _fire_resist { get => fireResist; set => fireResist = CheckNullValue(value); }
    private int fireResist;
    public int? _cold_resist { get => coldResist; set => coldResist = CheckNullValue(value); }
    private int coldResist;
    public int? _earth_resist { get => earthResist; set => earthResist = CheckNullValue(value); }
    private int earthResist;
    public int? _dark_resist { get => darkResist; set => darkResist = CheckNullValue(value); }
    private int darkResist;
    public int? _poison_resist { get => poisonResist; set => poisonResist = CheckNullValue(value); }
    private int poisonResist;
    public int? _evasion { get => evasion; set => evasion = CheckNullValue(value); }
    private int evasion;
    public string _option_name { get; set; }
}
