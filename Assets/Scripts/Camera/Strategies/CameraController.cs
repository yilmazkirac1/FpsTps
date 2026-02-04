using UnityEngine;

public enum CameraViewMode { TPS, FPS }

public class CameraController : MonoBehaviour
{
    [Header("References")]
    [Tooltip("Karakterin ana kökü (yaw burada döner).")]
    public Transform rotateTarget;

    [Tooltip("TPS hedefi (genelde rotateTarget olabilir).")]
    public Transform tpsTarget;

    [Tooltip("FPS'te kameranýn duracaðý nokta (Head/Neck altýndaki SightLook gibi).")]
    public Transform fpsViewPoint;

    public Camera cam;

    [Header("Sensitivity Settings")]
    public float fpsSensitivity = 150f;
    public float tpsSensitivity = 120f;

    public CameraViewMode CurrentMode { get; private set; } = CameraViewMode.TPS;

    private ICameraStrategy currentStrategy;

    void Awake()
    {
        if (cam == null) cam = GetComponent<Camera>();
        if (tpsTarget == null) tpsTarget = rotateTarget; // kolaylýk
    }

    void Start()
    {
        if (cam == null || rotateTarget == null)
        {
            enabled = false;
            return;
        }

        CurrentMode = CameraViewMode.TPS;
        SetStrategy(new TPSCameraStrategy(cam, tpsTarget, this));
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            CurrentMode = CameraViewMode.FPS;
            SetStrategy(new FPSCameraStrategy(cam, rotateTarget, fpsViewPoint, this));
        }

        if (Input.GetKeyDown(KeyCode.T))
        {
            CurrentMode = CameraViewMode.TPS;
            SetStrategy(new TPSCameraStrategy(cam, tpsTarget, this));
        }
    }

    void LateUpdate()
    {
        currentStrategy?.UpdateCamera();
    }

    public void SetStrategy(ICameraStrategy newStrategy)
    {
        currentStrategy?.OnExit();
        currentStrategy = newStrategy;
        currentStrategy.OnEnter();
    }
}
