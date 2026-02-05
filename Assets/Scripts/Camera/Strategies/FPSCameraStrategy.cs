using UnityEngine;

public class FPSCameraStrategy : ICameraStrategy
{
    private readonly Camera cam;
    private readonly Transform rotateTarget;   // yaw dönecek olan hedef (player root)
    private readonly Transform viewPoint;      // kameranýn duracaðý nokta (SightLook)
    private readonly CameraController controller;

    private float xRotation = 0f;

    // FPS'te kadraja kafa girmesin diye kamerayý viewPoint'in çok az gerisine alýyoruz.
    private const float cameraBackOffset = 0.06f; // 0.04 - 0.10 arasý deneyebilirsin

    public FPSCameraStrategy(Camera cam, Transform rotateTarget, Transform viewPoint, CameraController controller)
    {
        this.cam = cam;
        this.rotateTarget = rotateTarget;
        this.viewPoint = viewPoint; // null olabilir; aþaðýda fallback var
        this.controller = controller;
    }

    public void OnEnter()
    {
        //Cursor.lockState = CursorLockMode.Locked;
    }

    public void OnExit() { }

    public void UpdateCamera()
    {
       

        float mouseX = Input.GetAxis("Mouse X") * controller.fpsSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * controller.fpsSensitivity * Time.deltaTime;

        // Pitch
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -80f, 80f);

        // Yaw (gövde)
        rotateTarget.Rotate(Vector3.up * mouseX);

        // View point seçimi: varsa fpsViewPoint, yoksa rotateTarget
        Transform vp = viewPoint != null ? viewPoint : rotateTarget;

        // Kamerayý viewPoint'e koy ama biraz geriye al (kafaya girmesin)
        Vector3 forward = rotateTarget.forward;
        cam.transform.position = vp.position - forward * cameraBackOffset;

        // Kamera rotasyonu: pitch + karakter yaw
        cam.transform.rotation = Quaternion.Euler(xRotation, rotateTarget.eulerAngles.y, 0f);
    }
}
