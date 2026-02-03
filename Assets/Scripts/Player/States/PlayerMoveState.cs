using UnityEngine;

public class PlayerMoveState : IPlayerState
{
    private PlayerStateMachine sm;

    public PlayerMoveState(PlayerStateMachine stateMachine)
    {
        sm = stateMachine;
    }

    public void Enter() { }

    public void Update()
    {
        Vector2 move = sm.Input.MoveInput;

        sm.Motor.Move(move.x, move.y);

        if (sm.Input.CrouchPressed)
        {
            sm.ChangeState(new PlayerCrouchState(sm));
            return;
        }

        if (sm.Input.JumpPressed && sm.Motor.IsGrounded())
        {
            sm.Motor.Jump();
            sm.ChangeState(new PlayerJumpState(sm));
            return;
        }

        if (move.magnitude < 0.1f)
        {
            sm.ChangeState(new PlayerIdleState(sm));
        }
    }

    public void Exit() { }
}
