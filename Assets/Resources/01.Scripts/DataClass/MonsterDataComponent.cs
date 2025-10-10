using UnityEngine;
using System;

public class MonsterDataComponent : DecoratorDataComponent
{
    protected DecoratorDataComponent decoData;

    public DecoratorDataComponent Set(DecoratorDataComponent decoData)
    {
        this.decoData = decoData;
        return this;
    }

    public string _name { get { return monsterName; } set { monsterName = value; } }
    [SerializeField] private string monsterName;
    public ElementType? _element_type { get { return elementType; } set { elementType = CheckElementType(value); } }
    [SerializeField] private ElementType elementType;
    public MagicType? _magic1 { get { return magic1; } set { magic1 = CheckMagicType(value); } }
    [SerializeField] private MagicType magic1;
    public MagicType? _magic2 { get { return magic2; } set { magic2 = CheckMagicType(value); } }
    [SerializeField] private MagicType magic2;
    public string _comment { get { return comment; } set { comment = value; } }
    [SerializeField] private string comment;

    ElementType CheckElementType(ElementType? data)
    {
        return data ?? ElementType.NONE;
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
            case AttackType.FIRE: resValue = fireResist; break;
            case AttackType.COLD: resValue = coldResist; break;
            case AttackType.DARK: resValue = darkResist; break;
            case AttackType.MELEE: resValue = defense; break;
            case AttackType.POISON: resValue = poisonResist; break;
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
        decoData.Operation();
        decoData._current_hp += currentHp;
        decoData._current_mp += currentMp;
        decoData._max_hp += maxHp;
        decoData._max_mp += maxMp;
        decoData._min_attack += minAttack;
        decoData._max_attack += maxAttack;
        decoData._defense += defense;
        decoData._min_magic_attack += minMagicAttack;
        decoData._max_magic_attack += minMagicAttack;
        decoData._fire_resist += fireResist;
        decoData._cold_resist += coldResist;
        decoData._earth_resist += earthResist;
        decoData._dark_resist += darkResist;
        decoData._poison_resist += poisonResist;
        decoData._evasion += evasion;
    }

    public override void Revert()
    {
        decoData.Revert();
        decoData._current_hp -= currentHp;
        decoData._current_mp -= currentMp;
        decoData._max_hp -= maxHp;
        decoData._max_mp -= maxMp;
        decoData._min_attack -= minAttack;
        decoData._max_attack -= maxAttack;
        decoData._defense -= defense;
        decoData._min_magic_attack -= minMagicAttack;
        decoData._max_magic_attack -= minMagicAttack;
        decoData._fire_resist -= fireResist;
        decoData._cold_resist -= coldResist;
        decoData._earth_resist -= earthResist;
        decoData._dark_resist -= darkResist;
        decoData._poison_resist -= poisonResist;
        decoData._evasion -= evasion;
    }
}
