using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerDataComponent : DecoratorDataComponent
{
    public Action helmetEquipAction;
    public Action armourEquipAction;
    public Action shieldEquipAction;

    public HELMET_TYPE _helmetType { get { return helmetType; } set { helmetType = value; helmetEquipAction.Invoke(); } }
    [SerializeField] private HELMET_TYPE helmetType;
    public ARMOUR_TYPE _armourType { get { return armourType; } set { armourType = value; armourEquipAction.Invoke(); } }
    [SerializeField] private ARMOUR_TYPE armourType;
    public SHIELD_TYPE _shieldType { get { return shieldType; } set { shieldType = value; shieldEquipAction.Invoke(); } }
    [SerializeField] private SHIELD_TYPE shieldType;
    public PLAYER_STATE _playerState { get { return playerState; } set { playerState = value; } }
    [SerializeField] private PLAYER_STATE playerState;
    public Vector3 _dir { get { return dir; } set { dir = value; } }
    [SerializeField] private Vector3 dir;
    //
    public int? _level { get { return level; } set { level = CheckNullValue(value); } }
    [SerializeField] private int level;
    public int? _current_hp { get { return currentHp; } set { currentHp = CheckNullValue(value); } }
    [SerializeField] private int currentHp;
    public int? _current_mp { get { return currentMp; } set { currentMp = CheckNullValue(value); } }
    [SerializeField] private int currentMp;
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

    public override void Operation() { }

    public override void Revert() { }
}

public enum HELMET_TYPE
{
    helmet1,
    helmet2,
    helmet3,
    helmet4,
    helmet5,
    helmet6,
    helmet7,
    helmet8,
    helmet9,
    helmet10,
    helmet11,
    helmet12,
    helmet13,
    helmet14,
    helmet15,
    helmet16,
    helmet17,
    helmet18,
    none,
}

public enum ARMOUR_TYPE
{
    chainmail1,
    chainmail2,
    chainmail3,
    crystalplatemail1,
    crystalplatemail2,
    dragon_armour1,
    dragon_armour2,
    dragon_armour3,
    dragon_armour4,
    dragon_armour5,
    leader_armour1,
    leader_armour2,
    platemail1,
    platemail2,
    ringmail1,
    ringmail2,
    ringmail3,
    robe1,
    robe2,
    robe3,
    robe4,
    robe5,
    robe6,
    robe7,
    scalemail1,
    scalemail2,
    troll_leader_armour1,
    troll_leader_armour2,
    none,
}

public enum SHIELD_TYPE
{
    shield1,
    shield2,
    shield3,
    shield4,
    shield5,
    shield6,
    shield7,
    shield8,
    shield9,
    shield10,
    none,
}

public enum PLAYER_STATE
{
    IDLE = 0,
    MOVE = 1,
    ATTACK = 2,
    DIE = 3,
    ENDTURN = 4,
}
