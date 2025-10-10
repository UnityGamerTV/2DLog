using System;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class PlayerAttackState : MonoBehaviour, IState
{
    [Singleton(typeof(EventManager))] private EventManager eventManager;
    [FindComponents("Player")] private PlayerController controller;

    private float delayTime = 0.5f;
    private bool isAttack = false;

    public void Init()
    {
        InjectUtil.InjectSingleton(this);
        InjectUtil.InjectComponents(this);
    }

    public void OnStateEnter()
    {
        if (isAttack)
            return;

        isAttack = true;
        controller.PlayAnimation(PLAYER_STATE.ATTACK);
        OnStateUpdate();
    }

    public void OnStateUpdate() => Attack().Forget();

    private async UniTaskVoid Attack()
    {
        await UniTask.Delay(TimeSpan.FromSeconds(delayTime),cancellationToken : controller.GetCancellationTokenOnDestroy());
        OnStateExit();
    }

    public void OnStateExit()
    {
        isAttack = false;
        eventManager.PostNotification(EVENT_PLAYER.PLAYER_ATTACK_COMPLETE, controller);
    }
}
