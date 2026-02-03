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
    #endregion

    #region Private
    private CharacterController controller;
    private Vector3 velocity;
    private bool isGrounded;
    #endregion

    void Awake()
    {
        controller = GetComponent<CharacterController>();
    }

    #region Motor API (STATE'LER ÇAÐIRIR)

    public void CheckGround()
    {
        isGrounded = controller.isGrounded;

        if (isGrounded && velocity.y < 0)
            velocity.y = -2f;
    }

    public void Move(float x, float z)
    {
        Vector3 move =
            cameraTransform.right * x +
            cameraTransform.forward * z;

        move.y = 0f;

        float speed = Input.GetKey(KeyCode.LeftShift) ? runSpeed : walkSpeed;
        float control = isGrounded ? 1f : airControlMultiplier;

        controller.Move(move * speed * control * Time.deltaTime);
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

    #endregion
}
