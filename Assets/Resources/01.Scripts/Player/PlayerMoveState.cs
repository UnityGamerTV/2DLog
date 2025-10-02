using DG.Tweening;
using UnityEngine;
using Cysharp.Threading.Tasks;
using System.Threading;
using System;

public class PlayerMoveState : IState
{
    [Singleton(typeof(EventManager))] private EventManager eventManager;

    private CancellationTokenSource cancellationTokenSource;
    private PlayerController controller;
    private bool isMove;

    public PlayerMoveState(PlayerController controller)
    {
        this.controller = controller;
        isMove = false;
        cancellationTokenSource = new CancellationTokenSource();
        InjectUtil.InjectSingleton(this);
    }

    public void OnStateEnter()
    {
        if (isMove)
            return;

        isMove = true;
        controller.PlayAnimation(PLAYER_STATE.MOVE);
        OnStateUpdate();
        
        LogUtil.Log("isMove :" + isMove);
    }

    public void OnStateUpdate() => OnMove();
    
    private void OnMove()
    {
        Vector3 playerPos = controller.gameObject.transform.position; // 나중에 맵 매니저에서 관리할수도 있음
        Vector3 newPos = playerPos + controller.GetMoveDir();
        controller.gameObject.transform.DOMove(newPos, 0.5f).OnComplete(OnStateExit);
    }

    public void OnStateExit()
    {
        isMove = false;
        LogUtil.LogError("OnStateExit");
        //controller.HandIdle();
        //eventManager.PostNotification(EVENT_PLAYER.PLAYER_MOVE_COMPLETE, controller);
    }
}
