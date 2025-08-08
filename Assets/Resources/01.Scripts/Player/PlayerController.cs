using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerController : MonoBehaviour,IController
{
    [SerializeField] private PlayerService service;

    /// <summary>
    /// 임시 MapTest에서 사용함 
    /// 추후 테스트가 필요 없을 경우 삭제해도 됨
    /// </summary>
    public Animator GetAnimator() => service.GetAnimator();    
    //
    public void Init() => service.Init();
    //
    public void PlayAnim0() => service.PlayAnim0();
    public void PlayAnim1() => service.PlayAnim1();
    public void PlayAnim2() => service.PlayAnim2();
    public void PlayAnim3() => service.PlayAnim3();


    // 받은 string 값 또는 enum 값을 Model 에 넘겨주면 자동으로 스프라이트 변경 됨
    public void EquipHelmet(string helmetSpriteName) => service.EquipHelmet(helmetSpriteName);
    public void EquipArmour(string armourSpriteName) => service.EquipArmour(armourSpriteName);
    public void EquipShield(string shieldSpriteName) => service.EquipShield(shieldSpriteName);

    // 플레이어 상태 관리용
    public void SetPlayerState(PLAYER_STATE playerState) => service.SetPlayerState(playerState);


    public void Release()
    {
        
    }
}

