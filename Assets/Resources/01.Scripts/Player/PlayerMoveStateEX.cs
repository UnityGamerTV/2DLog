using DG.Tweening;
using UnityEngine;
using System.Threading;
using System;
using Cysharp.Threading;
using Cysharp.Threading.Tasks;

public class PlayerMoveStateEX : IState
{
    [Singleton(typeof(EventManager))] private EventManager eventManager;

    private PlayerController controller;
    private bool isMove;

    public PlayerMoveStateEX(PlayerController controller)
    {
        this.controller = controller;
        isMove = false;
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

    public void OnStateUpdate() => OnMove().Forget();
    
    private async UniTaskVoid OnMove()
    {
        Vector3 playerPos = controller.gameObject.transform.position; // 나중에 맵 매니저에서 관리할수도 있음
        Vector3 newPos = playerPos + controller.GetMoveDir();
        await controller.gameObject.transform.DOMove(newPos, 0.5f);
    }

    public void OnStateExit()
    {
        isMove = false;
        //LogUtil.LogError("OnStateExit");
        eventManager.PostNotification(EVENT_PLAYER.PLAYER_MOVE_COMPLETE, controller);
    }
}
