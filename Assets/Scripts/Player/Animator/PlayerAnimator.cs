using UnityEngine;


public class PlayerAnimator : MonoBehaviour
{
    [Header("Refs")]
    public PlayerStateMachine stateMachine;   // Inspector’dan baðla
    public PlayerMotor motor;                 // Inspector’dan baðla
    public PlayerInputHandler input;          // Inspector’dan baðla

    [Header("Tuning")]
    public float speedDamp = 10f;             // geçiþ yumuþatma

    private Animator anim;

    // Hash (performans + typo önler)
    private static readonly int SpeedHash = Animator.StringToHash("Speed");
    private static readonly int GroundHash = Animator.StringToHash("IsGrounded");
    private static readonly int CrouchHash = Animator.StringToHash("IsCrouching");
    private static readonly int JumpHash = Animator.StringToHash("Jump");
    private static readonly int RollHash = Animator.StringToHash("Roll");
    private static readonly int YVelHash = Animator.StringToHash("YVelocity");

    void Awake()
    {
        anim = GetComponentInChildren<Animator>();

        if (!stateMachine) stateMachine = GetComponent<PlayerStateMachine>();
        if (!motor) motor = GetComponent<PlayerMotor>();
        if (!input) input = GetComponent<PlayerInputHandler>();
    }

    void Update()
    {
        // Ground
        anim.SetBool(GroundHash, motor.IsGrounded());

        // Speed (0..1)
        // Shift basýlýysa koþu tarafýna yakýn, deðilse yürüme tarafýna yakýn olacak þekilde normalize ediyoruz
        Vector2 move = input.MoveInput;
        float raw = Mathf.Clamp01(move.magnitude);

        float targetSpeed01 = raw;
        // koþu varsa 1'e yaklaþsýn, yoksa 0.5 civarýnda kalsýn gibi bir his (blend tree ayarýna göre deðiþtir)
        if (input.RunPressed && raw > 0.1f)
            targetSpeed01 = Mathf.Lerp(0.5f, 1f, raw);
        else
            targetSpeed01 = Mathf.Lerp(0f, 0.6f, raw);

        float current = anim.GetFloat(SpeedHash);
        float smoothed = Mathf.Lerp(current, targetSpeed01, Time.deltaTime * speedDamp);
        anim.SetFloat(SpeedHash, smoothed);

        // Crouch bool (state bazlý istiyorsan bunu state’ten set edeceðiz; þimdilik input üzerinden de gider)
        // En profesyoneli: state enter/exit’te set etmek (aþaðýda göstereceðim).
        anim.SetBool(CrouchHash, input.CrouchPressed);

        // YVelocity (opsiyonel) — motor içinde dikey hýzýný expose edersen baðlarýz
        // anim.SetFloat(YVelHash, motor.VerticalVelocity);
    }

    // Bu fonksiyonu state’lerden çaðýracaðýz:
    public void TriggerJump()
    {
        anim.ResetTrigger(JumpHash);
        anim.SetTrigger(JumpHash);
    }
    public void TriggerRoll()
    {
        anim.ResetTrigger(RollHash);
        anim.SetTrigger(RollHash);
    }
    public void SetCrouch(bool value)
    {
        anim.SetBool(CrouchHash, value);
    }
}
