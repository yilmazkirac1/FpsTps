using UnityEngine;
public class PlayerJumpState : IPlayerState
{
    private PlayerStateMachine sm;

    public PlayerJumpState(PlayerStateMachine stateMachine)
    {
        sm = stateMachine;
    }

    public void Enter() { }

    public void Update()
    {
        // Ýstersen havada hareket:
        Vector2 move = sm.Input.MoveInput;
        sm.Motor.Move(move.x, move.y);

        if (sm.Motor.IsGrounded())
        {
            sm.ChangeState(new PlayerIdleState(sm));
        }
    }

    public void Exit() { }
}

