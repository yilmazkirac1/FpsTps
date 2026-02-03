using UnityEngine;

public class PlayerRollState : IPlayerState
{
    PlayerStateMachine sm;
    public PlayerRollState(PlayerStateMachine stateMachine)
    {
        sm = stateMachine;
    }
    public void Enter()
    {
        sm.PlayerAnimator.TriggerRoll();
    }
    public void Update()
    {
       
    }
    public void Exit()
    {
       
    }     
}
