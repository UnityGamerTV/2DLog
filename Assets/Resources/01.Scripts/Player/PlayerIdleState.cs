public class PlayerIdleState : IState
{
    private PlayerController controller;

    public PlayerIdleState(PlayerController controller)
    {
        this.controller = controller;
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
