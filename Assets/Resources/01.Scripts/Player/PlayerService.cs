using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerService : MonoBehaviour
{
    [SerializeField] private PlayerView view;
    [SerializeField] private PlayerDataComponent model;

    public Animator GetAnimator()
    {
        return view._bodyAnimator;
    }

    public void Init()
    {
        view.Init();
    }

    public void PlayAnim0() => view.PlayAnim0();
    public void PlayAnim1() => view.PlayAnim1();
    public void PlayAnim2() => view.PlayAnim2();
    public void PlayAnim3() => view.PlayAnim3();


    // 받은 string 값 또는 enum 값을 Model 에 넘겨주면 자동으로 스프라이트 변경 됨
    public void EquipHelmet(string helmetSpriteName)
    {
        if (helmetSpriteName.Equals("") || helmetSpriteName.Equals(String.Empty) || helmetSpriteName.Equals(" "))
        {
            model._shieldType = SHIELD_TYPE.none;
            return;
        }

        foreach (HELMET_TYPE one in Enum.GetValues(typeof(HELMET_TYPE)))
        {
            if (helmetSpriteName.Equals(one.ToString()))
            {
                model._helmetType = one;
                return;
            }
        }
        LogUtil.Log("장착할 수 있는 헬멧이 없습니다.");
    }

    public void EquipArmour(string armourSpriteName)
    {
        if (armourSpriteName.Equals("") || armourSpriteName.Equals(String.Empty) || armourSpriteName.Equals(" "))
        {
            model._shieldType = SHIELD_TYPE.none;
            return;
        }

        armourSpriteName = armourSpriteName.Replace(" ", "_");

        foreach (ARMOUR_TYPE one in Enum.GetValues(typeof(ARMOUR_TYPE)))
        {
            if (armourSpriteName.Equals(one.ToString()))
            {
                model._armourType = one;
                return;
            }
        }
        LogUtil.Log("장착할 수 있는 갑옷이 없습니다.");
    }

    public void EquipShield(string shieldSpriteName)
    {
        if (shieldSpriteName.Equals("") || shieldSpriteName.Equals(String.Empty) || shieldSpriteName.Equals(" "))
        {
            model._shieldType = SHIELD_TYPE.none;
            return;
        }

        foreach (SHIELD_TYPE one in Enum.GetValues(typeof(SHIELD_TYPE)))
        {
            if (shieldSpriteName.Equals(one.ToString()))
            {
                model._shieldType = one;
                return;
            }
        }
        LogUtil.Log("장착할 수 있는 쉴드가 없습니다.");
    }

    public void SetPlayerState(PLAYER_STATE playerState)
    {
        model._playerState = playerState;
        view.SetAnimation(playerState);
    }
}
