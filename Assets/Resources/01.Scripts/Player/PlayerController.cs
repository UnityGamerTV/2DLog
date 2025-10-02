using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerController : FieldObjBase, IController, IListener
{
    [Singleton(typeof(EventManager))] private EventManager eventManager;
    [Singleton(typeof(MapManager))] private MapManager mapManager;

    [FindComponents("Service"), SerializeField] private PlayerService service;
    [FindComponents("State"), SerializeField] private PlayerState state;

    public void Init()
    {
        InjectUtil.InjectSingleton(this);
        InjectUtil.InjectComponents(this);

        service.Init();

        eventManager.AddListener(EVENT_PLAYER.PLAYER_DOWN_MOVE, this);
        eventManager.AddListener(EVENT_PLAYER.PLAYER_UP_MOVE, this);
        eventManager.AddListener(EVENT_PLAYER.PLAYER_LEFT_MOVE, this);
        eventManager.AddListener(EVENT_PLAYER.PLAYER_RIGHT_MOVE, this);
        eventManager.AddListener(EVENT_PLAYER.PLAYER_MOVE_COMPLETE, this);

        HandIdle();
    }

    /// <summary>
    /// 임시 MapTest에서 사용함 
    /// 추후 테스트가 필요 없을 경우 삭제해도 됨
    /// </summary>
    /// 
    public Animator GetAnimator() => service.GetAnimator();
    //
    public void PlayAnim0() => service.PlayAnim0();
    public void PlayAnim1() => service.PlayAnim1();
    public void PlayAnim2() => service.PlayAnim2();
    public void PlayAnim3() => service.PlayAnim3();


    // 받은 string 값 또는 enum 값을 Model 에 넘겨주면 자동으로 스프라이트 변경 됨
    public void EquipHelmet(string helmetSpriteName) => service.EquipHelmet(helmetSpriteName);
    public void EquipArmour(string armourSpriteName) => service.EquipArmour(armourSpriteName);
    public void EquipShield(string shieldSpriteName) => service.EquipShield(shieldSpriteName);

    public void PlayAnimation(PLAYER_STATE playerState) => service.PlayAnimation(playerState);

    public void IsValidPosition(Vector3 dir) => service.IsValidPosition(dir);

    public void HandIdle() => service.HandIdle();

    // 이동 관련 모음
    #region PlayerMove

    public Vector3 GetMoveDir() => service.GetMoveDir();
    public void SetMoveDir(Vector3 dir) => service.SetMoveDir(dir);
    public void SetPlayerState(IState state) => service.SetPlayerState(state);
    public void DoPlayerState() => service.DoPlayerState();
    public void SetFilpXSprite(bool isFilp) => service.SetFilpXSprite(isFilp);

    #endregion

    void IListener.OnEvent<TEnum>(TEnum eventType, Component sender, object param)
    {
        switch (eventType)
        {
            case EVENT_PLAYER.PLAYER_DOWN_MOVE: IsValidPosition(Vector3.down); break;
            case EVENT_PLAYER.PLAYER_UP_MOVE: IsValidPosition(Vector3.up); break;
            case EVENT_PLAYER.PLAYER_LEFT_MOVE: IsValidPosition(Vector3.left); break;
            case EVENT_PLAYER.PLAYER_RIGHT_MOVE: IsValidPosition(Vector3.right); break;
            case EVENT_PLAYER.PLAYER_MOVE_COMPLETE: HandIdle(); break;
        }
    }

    public void Release()
    {
        
    }
}

