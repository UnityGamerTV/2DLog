using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackState : IState
{
    private PlayerController controller;

    public PlayerAttackState (PlayerController controller)
    {
        this.controller = controller;
    }

    public void OnStateEnter()
    {
        controller.PlayAnimation(PLAYER_STATE.ATTACK);
        OnStateUpdate();
    }

    public void OnStateExit()
    {
        
    }

    public void OnStateUpdate()
    {
        
    }
}
