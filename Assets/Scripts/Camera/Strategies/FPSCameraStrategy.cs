using UnityEngine;

public class FPSCameraStrategy : ICameraStrategy
{
    private Camera cam;
    private Transform target;
    private CameraController controller;

    private float xRotation = 0f;
    private Vector3 headOffset = new Vector3(0, 1.6f, 0);

    public FPSCameraStrategy(Camera cam, Transform target, CameraController controller)
    {
        this.cam = cam;
        this.target = target;
        this.controller = controller;
    }

    public void OnEnter()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void OnExit() { }

    public void UpdateCamera()
    {
        cam.transform.position = target.position + headOffset;

        float mouseX = Input.GetAxis("Mouse X") * controller.fpsSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * controller.fpsSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -80f, 80f);

        cam.transform.rotation =
            Quaternion.Euler(xRotation, target.eulerAngles.y, 0f);

        target.Rotate(Vector3.up * mouseX);
    }
}
