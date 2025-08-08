public class PlayerIdleState : IState
{
    private PlayerController controller;

    public PlayerIdleState(PlayerController controller)
    {
        this.controller = controller;
    }

    public void OnStateEnter()
    {
        controller.SetPlayerState(PLAYER_STATE.IDLE);
    }

    public void OnStateExit()
    {
        throw new System.NotImplementedException();
    }

    public void OnStateUpdate()
    {
        throw new System.NotImplementedException();
    }
}
