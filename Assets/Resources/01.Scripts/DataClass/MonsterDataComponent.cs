using UnityEngine;
using System;

public class MonsterDataComponent : DecoratorDataComponent
{
    public string _name { get { return monsterName; } set { monsterName = value; } }
    [SerializeField] private string monsterName;
    public int _current_hp { get { return currentHp; } set { currentHp = value; } }
    [SerializeField] private int currentHp;
    public MonsterAttackType? _attackType { get { return attackType; } set { attackType = CheckMonsterAttackType(value); } }
    [SerializeField] private MonsterAttackType attackType;
    public MagicType? _magic1 { get { return magic1; } set { magic1 = CheckMagicType(value); } }
    [SerializeField] private MagicType magic1;
    public MagicType? _magic2 { get { return magic2; } set { magic2 = CheckMagicType(value); } }
    [SerializeField] private MagicType magic2;
    public string _comment { get { return comment; } set { comment = value; } }
    [SerializeField] private string comment;

    MonsterAttackType CheckMonsterAttackType(MonsterAttackType? data)
    {
        return data ?? MonsterAttackType.NONE;
    }

    MagicType CheckMagicType(MagicType? data)
    {
        return data ?? MagicType.NONE;
    }


    public void OnDamaged(AttackType attackType, int damage)
    {
        int resValue = CalResistanceValue(attackType); //속성 확인 후 저항값 
        CalDamaged(damage, resValue);         //대미지 계산 
    }

    private int CalResistanceValue(AttackType attackType)
    {
        int resValue = 0;

        switch (attackType)
        {
            case AttackType.FIRE: resValue = fireRes; break;
            case AttackType.COLD: resValue = coldRes; break;
            case AttackType.DARK: resValue = darkRes; break;
            case AttackType.MELEE: resValue = defence; break;
            case AttackType.POISON: resValue = poisonRes; break;
        }
        return resValue;
    }

    private void CalDamaged(int damage, int resistValue)
    {
        int calDamage = damage - resistValue;
        int tempHp = currentHp;
        if (calDamage > 0)
        {
            tempHp -= calDamage;
            if (tempHp < 0)
            {
                tempHp = 0;
                // 죽었다는 광역 이벤트 추가
            }

            currentHp = tempHp; // view 에 hp바 적용 델리게이트 추가
        }
    }

    public override void Operation()
    {
        throw new NotImplementedException();
    }

    public override void Revert()
    {
        throw new NotImplementedException();
    }
}
