using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMotor : MonoBehaviour
{
    #region Settings
    public float walkSpeed = 5f;
    public float runSpeed = 8f;
    public float airControlMultiplier = 0.5f;
    public float jumpHeight = 1.5f;
    public float gravity = -9.81f;
    public Transform cameraTransform;
    [Header("Rotation")]
    public float rotationSpeed = 12f;

    #endregion

    #region Private
    private CharacterController controller;
    private Vector3 velocity;
    private bool isGrounded;
    private PlayerInputHandler playerInputHandler;
    #endregion

    void Awake()
    {
        controller = GetComponent<CharacterController>();
        playerInputHandler = GetComponent<PlayerInputHandler>();
    }

    #region Motor API (STATE'LER ÇAÐIRIR)

    public void CheckGround()
    {
        isGrounded = controller.isGrounded;

        if (isGrounded && velocity.y < 0)
            velocity.y = -2f;
    }

    public void Move(float x, float z, bool rotateToMoveDirection)
    {
        if (InventoryUI.IsOpen) return;

        Vector3 moveDirection =
            cameraTransform.right * x +
            cameraTransform.forward * z;

        moveDirection.y = 0f;

        float speed = playerInputHandler.RunPressed ? runSpeed : walkSpeed;
        float control = isGrounded ? 1f : airControlMultiplier;

        // ? TPS: karakteri hareket yönüne döndür
        if (rotateToMoveDirection && moveDirection.sqrMagnitude > 0.0001f)
        {
            Quaternion targetRot = Quaternion.LookRotation(moveDirection.normalized, Vector3.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, rotationSpeed * Time.deltaTime);
        }

        controller.Move(moveDirection.normalized * speed * control * Time.deltaTime);
    }


    public void Jump()
    {
        if (!isGrounded) return;

        velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
    }

    public void ApplyGravity()
    {
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    public bool IsGrounded()
    {
        return isGrounded;
    }
    public void RollMove(Vector3 worldDir, float rollSpeed)
    {
        worldDir.y = 0f;
        if (worldDir.sqrMagnitude < 0.0001f) return;

        controller.Move(worldDir.normalized * rollSpeed * Time.deltaTime);
    }

    #endregion

}
