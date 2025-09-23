using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerDataComponent : MonoBehaviour
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
