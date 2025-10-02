using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState : MonoBehaviour
{
    [FindComponents("Player"), SerializeField] private PlayerController controller;

    public IState _idleState { get { return idleState; } }
    private IState idleState;
    public IState _moveState { get { return moveState; } }
    private IState moveState;
    public IState _attackState { get { return attackState; } }
    private IState attackState;

    private IState dieState;
    private IState endTurnState;
    private IState abilityState;

    public void Init()
    {
        InjectUtil.InjectComponents(this);

        idleState = new PlayerIdleState(controller);
        moveState = new PlayerMoveState(controller);
        attackState = new PlayerAttackState(controller);
    }
}
