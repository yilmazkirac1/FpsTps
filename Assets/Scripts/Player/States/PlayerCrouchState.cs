using UnityEngine;

public class PlayerCrouchState : IPlayerState
{
    private PlayerStateMachine sm;

    public PlayerCrouchState(PlayerStateMachine stateMachine)
    {
        sm = stateMachine;
    }

    public void Enter()
    {
        sm.GetComponent<PlayerAnimator>().SetCrouch(true);
    }

    public void Update()
    {
        Vector2 move = sm.Input.MoveInput;

        bool rotate = sm.CameraController.CurrentMode == CameraViewMode.TPS;
        sm.Motor.Move(move.x, move.y, rotate);

        if (sm.Input.RollPressed && sm.Motor.IsGrounded())
        {
            sm.ChangeState(new PlayerRollState(sm));
            return;
        }

        // ? Jump yok (bilerek çaðrýlmýyor)

        if (!sm.Input.CrouchPressed)
        {
            sm.ChangeState(new PlayerIdleState(sm));
        }
    }

    public void Exit()
    {
        sm.GetComponent<PlayerAnimator>().SetCrouch(false);
    }
}

