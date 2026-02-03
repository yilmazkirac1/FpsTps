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
        // collider küçültme / animasyon burada
    }

    public void Update()
    {
        Vector2 move = sm.Input.MoveInput;
        sm.Motor.Move(move.x, move.y);

        // ? Jump yok (bilerek çaðrýlmýyor)

        if (!sm.Input.CrouchPressed)
        {
            sm.ChangeState(new PlayerIdleState(sm));
        }
    }

    public void Exit()
    {
        // collider eski hal
    }
}

