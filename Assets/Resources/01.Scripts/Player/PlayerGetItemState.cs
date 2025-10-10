using System;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

public class PlayerGetItemState : MonoBehaviour, IState
{
    [Singleton(typeof(FieldManager))] private FieldManager fieldManager;
    [Singleton(typeof(EventManager))] private EventManager eventManager;
    [FindComponents("Player")] private PlayerController controller;

    private float moveDelayTime = 0.5f;
    private float getDelayTime = 0.25f;
    private bool isMove = false;

    public void Init()
    {
        InjectUtil.InjectSingleton(this);
        InjectUtil.InjectComponents(this);
    }

    public void OnStateEnter()
    {
        if (isMove)
            return;

        isMove = true;
        controller.PlayAnimation(PLAYER_STATE.MOVE);
        OnStateUpdate();
    }
    public void OnStateUpdate() => GetItem();

    private void GetItem()
    {
        Move().Forget();
        Get().Forget();
    }

    private async UniTaskVoid Move()
    {
        Vector3 playerPos = controller.gameObject.transform.position; // 나중에 맵 매니저에서 관리할수도 있음
        Vector3 newPos = playerPos + controller.GetMoveDir();
        await controller.gameObject.transform
            .DOMove(newPos, moveDelayTime)
            .ToUniTask(cancellationToken: controller.GetCancellationTokenOnDestroy());
        OnStateExit();
    }

    private async UniTaskVoid Get()
    {
        await UniTask.Delay(TimeSpan.FromSeconds(getDelayTime), cancellationToken: controller.GetCancellationTokenOnDestroy());
        fieldManager._getItemController.OnPlayerTouch();
    }

    public void OnStateExit()
    {
        isMove = false;
        eventManager.PostNotification(EVENT_PLAYER.PLAYER_GET_ITEM_COMPLETE, this);
    }

}
