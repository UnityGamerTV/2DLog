using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 스킬 레벨
public partial class PlayerDataComponent : DecoratorDataComponent
{
    public int? _arrow_skill_level { get { return arrowSkillLevel; } set { arrowSkillLevel = CheckNullValue(value); } }
    [SerializeField] private int arrowSkillLevel;
    public int? _axe_skill_level { get { return axeSkillLevel; } set { axeSkillLevel = CheckNullValue(value); } }
    [SerializeField] private int axeSkillLevel;
    public int? _mace_skill_level { get { return maceSkillLevel; } set { maceSkillLevel = CheckNullValue(value); } }
    [SerializeField] private int maceSkillLevel;
    public int? _spear_skill_level { get { return spearSkillLevel; } set { spearSkillLevel = CheckNullValue(value); } }
    [SerializeField] private int spearSkillLevel;
    public int? _staff_skill_level { get { return staffSkillLevel; } set { staffSkillLevel = CheckNullValue(value); } }
    [SerializeField] private int staffSkillLevel;
    public int? _sword_skill_level { get { return swordSkillLevel; } set { swordSkillLevel = CheckNullValue(value); } }
    [SerializeField] private int swordSkillLevel;
    public int? _cold_skill_level { get { return coldSkillLevel; } set { coldSkillLevel = CheckNullValue(value); } }
    [SerializeField] private int coldSkillLevel;
    public int? _earth_skill_level { get { return earthSkillLevel; } set { earthSkillLevel = CheckNullValue(value); } }
    [SerializeField] private int earthSkillLevel;
    public int? _dark_skill_level { get { return darkSkillLevel; } set { darkSkillLevel = CheckNullValue(value); } }
    [SerializeField] private int darkSkillLevel;
    public int? _fire_skill_level { get { return fireSkillLevel; } set { fireSkillLevel = CheckNullValue(value); } }
    [SerializeField] private int fireSkillLevel;
    public int? _poison_skill_level { get { return poisonSkillLevel; } set { poisonSkillLevel = CheckNullValue(value); } }
    [SerializeField] private int poisonSkillLevel;
}
