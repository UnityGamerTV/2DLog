using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

public class PlayerMoveState : MonoBehaviour, IState
{
    [Singleton(typeof(EventManager))] private EventManager eventManager;
    [FindComponents("Player")] private PlayerController controller;

    private float delayTime = 0.5f;
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

    public void OnStateUpdate() => Move().Forget();

    private async UniTaskVoid Move()
    {
        Vector3 playerPos = controller.gameObject.transform.position; // 나중에 맵 매니저에서 관리할수도 있음
        Vector3 newPos = playerPos + controller.GetMoveDir();
        await controller.gameObject.transform
            .DOMove(newPos, delayTime)
            .ToUniTask(cancellationToken : controller.GetCancellationTokenOnDestroy());
        OnStateExit();
    }

    public void OnStateExit()
    {
        isMove = false;
        eventManager.PostNotification(EVENT_PLAYER.PLAYER_MOVE_COMPLETE, this);
    }

}
