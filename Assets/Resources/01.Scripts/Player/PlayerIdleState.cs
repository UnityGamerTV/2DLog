using UnityEngine;

public class PlayerIdleState : MonoBehaviour, IState
{
    [FindComponents("Player")] private PlayerController controller;

    public void Init()
    {
        InjectUtil.InjectComponents(this);
    }

    public void OnStateEnter()
    {
        controller.PlayAnimation(PLAYER_STATE.IDLE);
        OnStateUpdate();
    }

    public void OnStateUpdate()
    {

    }

    public void OnStateExit()
    {
        
    }

}
