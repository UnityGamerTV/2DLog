using System;

[Serializable]
public class RandartData
{
    public int _id { get; set; }
    public string _name { get; set; }
    public int? _max_hp { get { return maxHp; } set { maxHp = CheckNullValue(value); } }
    private int maxHp;
    public int? _max_mp { get { return maxMp; } set { maxMp = CheckNullValue(value); } }
    private int maxMp;
    public int? _min_attack { get { return minAttack; } set { minAttack = CheckNullValue(value); } }
    private int minAttack;
    public int? _max_attack { get { return maxAttack; } set { maxAttack = CheckNullValue(value); } }
    private int maxAttack;
    public int? _defense { get { return defense; } set { defense = CheckNullValue(value); } }
    private int defense;
    public int? _min_magic_attack { get { return minMagicAttack; } set { minMagicAttack = CheckNullValue(value); } }
    private int minMagicAttack;
    public int? _max_magic_attack { get { return maxMagicAttack; } set { maxMagicAttack = CheckNullValue(value); } }
    private int maxMagicAttack;
    public int? _fire_resist { get { return fireResist; } set { fireResist = CheckNullValue(value); } }
    private int fireResist;
    public int? _cold_resist { get { return coldResist; } set { coldResist = CheckNullValue(value); } }
    private int coldResist;
    public int? _earth_resist { get { return earthResist; } set { earthResist = CheckNullValue(value); } }
    private int earthResist;
    public int? _dark_resist { get { return darkResist; } set { darkResist = CheckNullValue(value); } }
    private int darkResist;
    public int? _poison_resist { get { return poisonResist; } set { poisonResist = CheckNullValue(value); } }
    private int poisonResist;
    public int? _evasion { get { return evasion; } set { evasion = CheckNullValue(value); } }
    private int evasion;
    public string _option_name { get; set; }

    T CheckNullValue<T>(T? data) where T : struct
    {
        return data ?? default;
    }
}
