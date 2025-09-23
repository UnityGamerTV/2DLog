using DG.Tweening;
using UnityEngine;

public class PlayerMoveState : IState
{
    [Singleton(typeof(EventManager))] private EventManager eventManager;

    private PlayerController controller;
    private bool isMove;

    public PlayerMoveState(PlayerController controller)
    {
        this.controller = controller;
        isMove = false;
        InjectUtil.InjectSingleton(this);
    }

    public void OnStateEnter()
    {
        if (isMove)
            return;
        else
        {
            isMove = true;
            controller.PlayAnimation(PLAYER_STATE.MOVE);
            OnStateUpdate();
        }
        LogUtil.LogError("isMove :" + isMove);
    }

    public void OnStateUpdate()
    {
        isMove = true;
        Vector3 playerPos = controller.gameObject.transform.position;
        Vector3 newPos = playerPos + controller.GetMoveDir();
        controller.gameObject.transform.DOMove(newPos, 0.5f)
            .OnComplete(OnStateExit);
    }

    public void OnStateExit()
    {
        isMove = false;
        controller.HandIdle();
        eventManager.PostNotification(EVENT_PLAYER.PLAYER_MOVE_COMPLETE, controller);
    }
}
