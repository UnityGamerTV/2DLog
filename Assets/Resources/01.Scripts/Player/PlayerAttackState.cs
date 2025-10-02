using System;
using System.Threading;
using Cysharp.Threading.Tasks;

public class PlayerAttackState : IState, IDisposable
{
    [Singleton(typeof(EventManager))] private EventManager eventManager;

    private PlayerController controller;
    private CancellationTokenSource attackCts;
    private bool isAttack;

    public PlayerAttackState (PlayerController controller)
    {
        this.controller = controller;
        isAttack = false;
        InjectUtil.InjectSingleton(this);
    }

    public void OnStateEnter()
    {
        if (isAttack)
            return;

        isAttack = true;
        DisposeToken();
        attackCts = new();
        controller.PlayAnimation(PLAYER_STATE.ATTACK);
        OnStateUpdate();

        LogUtil.Log("isAttack :" + isAttack);
    }

    public void OnStateUpdate() => OnAttack(attackCts.Token).Forget();
    
    private async UniTaskVoid OnAttack(CancellationToken token)
    {
        await UniTask.Delay(TimeSpan.FromSeconds(0.5f), cancellationToken: token);
        OnStateExit();
    }

    public void OnStateExit()
    {
        isAttack = false;
        controller.HandIdle();
        eventManager.PostNotification(EVENT_PLAYER.PLAYER_ATTACK_COMPLETE, controller);
    }

    private void DisposeToken()
    {
        attackCts?.Cancel();
        attackCts?.Dispose();
        attackCts = null;
    }

    public void Dispose() => DisposeToken();
}
