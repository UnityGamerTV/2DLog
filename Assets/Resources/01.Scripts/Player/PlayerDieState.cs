using System;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class PlayerDieState : MonoBehaviour, IState
{
    [Singleton(typeof(EventManager))] private EventManager eventManager;
    [FindComponents("Player")] private PlayerController controller;

    private float delayTime = 0.5f;
    private bool isDie = false;

    public void Init()
    {
        InjectUtil.InjectComponents(this);
    }

    public void OnStateEnter()
    {
        if (isDie)
            return;

        isDie = true;
        controller.PlayAnimation(PLAYER_STATE.DIE);
        OnStateUpdate();
    }

    public void OnStateUpdate() => Die().Forget();
    
    private async UniTaskVoid Die()
    {
        await UniTask.Delay(TimeSpan.FromSeconds(delayTime), cancellationToken: controller.GetCancellationTokenOnDestroy());
        OnStateExit();
    }

    public void OnStateExit()
    {
        isDie = false;
        eventManager.PostNotification(EVENT_PLAYER.PLAYER_ATTACK_COMPLETE, controller);
    }
}
