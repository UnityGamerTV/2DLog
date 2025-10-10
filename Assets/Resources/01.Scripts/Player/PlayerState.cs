using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState : MonoBehaviour
{
    [FindComponents("Player"), SerializeField] private PlayerController controller;
    [FindComponents("IdleState"), SerializeField] private PlayerIdleState idleState;
    [FindComponents("MoveState"), SerializeField] private PlayerMoveState moveState;
    [FindComponents("AttackState"), SerializeField] private PlayerAttackState attackState;
    [FindComponents("DieState"), SerializeField] private PlayerDieState dieState;
    [FindComponents("GetItemState"), SerializeField] private PlayerGetItemState getItemState;

    public PlayerIdleState _idleState { get { return idleState; } set { idleState = value; } }
    public PlayerMoveState _moveState { get { return moveState; } set { moveState = value; } }
    public PlayerAttackState _attackState { get { return attackState; } set { attackState = value; } }
    public PlayerDieState _dieState { get { return dieState; } set { dieState = value; } }
    public PlayerGetItemState _getItemState { get { return getItemState; } set { getItemState = value; } }

    public void Init()
    {
        InjectUtil.InjectComponents(this);

        idleState.Init();
        moveState.Init();
        attackState.Init();
        dieState.Init();
        getItemState.Init();
    }
}
