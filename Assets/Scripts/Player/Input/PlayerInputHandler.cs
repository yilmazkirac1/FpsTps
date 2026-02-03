using UnityEngine;

public class PlayerInputHandler : MonoBehaviour
{
    public Vector2 MoveInput =>
        new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

    public bool RunPressed => Input.GetKey(KeyCode.LeftShift);
    public bool CrouchPressed => Input.GetKey(KeyCode.LeftControl);
    public bool JumpPressed => Input.GetButtonDown("Jump");

}
