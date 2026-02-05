using UnityEngine;

public class TPSCameraStrategy : ICameraStrategy
{
    private Camera cam;
    private Transform target;
    private CameraController controller;

    private float distance = 4f;
    private float yaw;
    private float pitch = 20f;
    private Vector3 posVelocity;
    private float smoothTime = 0.06f;

    public TPSCameraStrategy(Camera cam, Transform target, CameraController controller)
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
    

        yaw += Input.GetAxis("Mouse X") * controller.tpsSensitivity * Time.deltaTime;
        pitch -= Input.GetAxis("Mouse Y") * controller.tpsSensitivity * Time.deltaTime;
        pitch = Mathf.Clamp(pitch, -10f, 60f);

        Quaternion rot = Quaternion.Euler(pitch, yaw, 0);
        Vector3 offset = rot * new Vector3(0, 0, -distance);

        Vector3 desiredPos = target.position + offset;
        cam.transform.position = Vector3.SmoothDamp(
            cam.transform.position,
            desiredPos,
            ref posVelocity,
            smoothTime
        );
        cam.transform.LookAt(target.position + Vector3.up * 1.5f);

    }
}
