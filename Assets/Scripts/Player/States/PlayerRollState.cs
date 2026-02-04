using UnityEngine;

public class PlayerRollState : IPlayerState
{
    private readonly PlayerStateMachine sm;

    private float timer;
    private const float RollDuration = 0.55f; // anim sürene göre ayarla (0.45-0.7)
    private const float RollSpeed = 9.5f;     // hýz

    private Vector3 rollDir;

    public PlayerRollState(PlayerStateMachine stateMachine)
    {
        sm = stateMachine;
    }

    public void Enter()
    {
        // 1) Roll yönünü giriþ anýnda kilitle
        Vector2 move = sm.Input.MoveInput;

        // Input yoksa: karakterin baktýðý yöne roll
        if (move.sqrMagnitude < 0.01f)
        {
            rollDir = sm.transform.forward;
        }
        else
        {
            // Kameraya göre world yön
            Vector3 dir =
                sm.Motor.cameraTransform.right * move.x +
                sm.Motor.cameraTransform.forward * move.y;

            dir.y = 0f;
            rollDir = dir.normalized;
        }

        // 2) Animasyonu tetikle
        sm.PlayerAnimator.TriggerRoll();

        timer = 0f;
    }

    public void Update()
    {
        timer += Time.deltaTime;

        // Roll boyunca hareket
        sm.Motor.RollMove(rollDir, RollSpeed);

        // Roll bitti -> state deðiþtir
        if (timer >= RollDuration)
        {
            Vector2 move = sm.Input.MoveInput;
            if (move.magnitude > 0.1f) sm.ChangeState(new PlayerMoveState(sm));
            else sm.ChangeState(new PlayerIdleState(sm));
        }
    }

    public void Exit() { }
}
