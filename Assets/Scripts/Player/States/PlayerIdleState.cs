using UnityEngine;

public class PlayerIdleState : IPlayerState
{
    private PlayerStateMachine sm;

    public PlayerIdleState(PlayerStateMachine stateMachine)
    {
        sm = stateMachine;
    }

    public void Enter() { }

    public void Update()
    {
        Vector2 move = sm.Input.MoveInput;

        if (sm.Input.CrouchPressed)
        {
            sm.ChangeState(new PlayerCrouchState(sm));
            return;
        }

        if (sm.Input.JumpPressed && sm.Motor.IsGrounded())
        {
            sm.Motor.Jump();
            sm.GetComponent<PlayerAnimator>().TriggerJump();
            sm.ChangeState(new PlayerJumpState(sm));
            return;
        }


        if (move.magnitude > 0.1f)
        {
            sm.ChangeState(new PlayerMoveState(sm));
        }
    }

    public void Exit() { }
}
