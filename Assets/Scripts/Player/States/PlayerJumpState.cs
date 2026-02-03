using UnityEngine;
public class PlayerJumpState : IPlayerState
{
    private PlayerStateMachine sm;

    public PlayerJumpState(PlayerStateMachine stateMachine)
    {
        sm = stateMachine;
    }

    public void Enter()
    {
        sm.Motor.Jump();
        sm.PlayerAnimator.TriggerJump();
    }

    public void Update()
    {
        Vector2 move = sm.Input.MoveInput;

        bool rotate = sm.CameraController.CurrentMode == CameraViewMode.TPS;
        sm.Motor.Move(move.x, move.y, rotate);


        if (sm.Motor.IsGrounded())
        {
            sm.ChangeState(new PlayerIdleState(sm));
        }
    }

    public void Exit() { }
}

