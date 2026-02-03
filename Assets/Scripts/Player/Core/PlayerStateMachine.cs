using UnityEngine;

public class PlayerStateMachine : MonoBehaviour
{
    public PlayerMotor Motor { get; private set; }
    public PlayerInputHandler Input { get; private set; }

    private IPlayerState currentState;

    void Awake()
    {
        Motor = GetComponent<PlayerMotor>();
        Input = GetComponent<PlayerInputHandler>();
    }

    void Start()
    {
        ChangeState(new PlayerIdleState(this));
    }

    void Update()
    {
        Motor.CheckGround();
        currentState?.Update();
        Motor.ApplyGravity();
    }

    public void ChangeState(IPlayerState newState)
    {
        currentState?.Exit();
        currentState = newState;
        currentState.Enter();
    }
}
