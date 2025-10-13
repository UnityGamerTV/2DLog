using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 넘버링으로 구분하고 있기 때문에 중간에 추가하면 숫자가 밀림 주의
/// ParticleFactory 클래스 - SetData 메서드에서 사용 중
/// </summary>
public enum ParticleType 
{
    MagicEffect_cold1 = 0,
    MagicEffect_cold2,
    MagicEffect_cold3,
    MagicEffect_cold5,
    MagicEffect_cold5_Impact,
    MagicEffect_cold6,
    MagicEffect_cold7,
    MagicEffect_cold8,
    MagicEffect_cold8_Impact,
    MagicEffect_earth1,
    MagicEffect_earth1_Impact,
    MagicEffect_earth2,
    MagicEffect_earth3,
    MagicEffect_earth4,
    MagicEffect_earth5,
    MagicEffect_earth5_Impact,
    MagicEffect_fire1,
    MagicEffect_fire1_Impact,
    MagicEffect_fire2,
    MagicEffect_fire3,
    MagicEffect_fire4,
    MagicEffect_fire4_Impact,
    MagicEffect_fire5,
    MagicEffect_fire6,
    MagicEffect_fire7,
    MagicEffect_fire7_Impact,
    MagicEffect_nec1, //sum1
    MagicEffect_nec2,
    MagicEffect_nec3, //sum1
    MagicEffect_nec4,
    MagicEffect_nec5,
    MagicEffect_nec6,
    MagicEffect_poison1,
    MagicEffect_poison1_Impact,
    MagicEffect_poison2,
    MagicEffect_poison2_Impact,
    MagicEffect_poison3,
    MagicEffect_poison4,
    MagicEffect_poison5,
    MagicEffect_poison6,
    MagicEffect_sum1,
    MagicEffect_sum2, //sum1
    MagicEffect_sum3, //sum1
    MagicEffect_sum4, //sum1
    MagicEffect_sum5,
    MagicEffect_sum6, //sum5
    //
    BaseEffect_arrow1 = 100,
    BaseEffect_axe1,
    BaseEffect_mace1,
    BaseEffect_spear1,
    BaseEffect_staff1,
    BaseEffect_sword1,
    SkillEffect_arrow2,
    SkillEffect_axe2,
    SkillEffect_mace2,
    SkillEffect_spear2,
    SkillEffect_sword2,
    //
    ItemEffect_item1 = 200,
}
