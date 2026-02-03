using UnityEngine;

public enum CameraViewMode { TPS, FPS }

public class CameraController : MonoBehaviour
{
    [Header("References")]
    public Transform target;
    public Camera cam;

    [Header("Sensitivity Settings")]
    public float fpsSensitivity = 150f;
    public float tpsSensitivity = 120f;

    public CameraViewMode CurrentMode { get; private set; } = CameraViewMode.TPS;

    private ICameraStrategy currentStrategy;

    void Awake()
    {
        // Otomatik doldurma (Inspector boþsa)
        if (cam == null) cam = GetComponent<Camera>();

        Debug.Log($"[CameraController] Awake. cam={(cam ? cam.name : "NULL")} target={(target ? target.name : "NULL")}");
    }

    void Start()
    {
        if (cam == null || target == null)
        {
            Debug.LogError("[CameraController] cam veya target boþ! Inspector'dan atamalýsýn.");
            enabled = false;
            return;
        }

        // Oyun baþlarken TPS ile baþla
        CurrentMode = CameraViewMode.TPS;
        SetStrategy(new TPSCameraStrategy(cam, target, this));

        Debug.Log("[CameraController] Start -> TPS strategy set edildi.");
    }
    void Update()
    {
        // sadece mod deðiþtirme input'u burada kalsýn
        if (Input.GetKeyDown(KeyCode.F))
        {
            CurrentMode = CameraViewMode.FPS;
            SetStrategy(new FPSCameraStrategy(cam, target, this));
        }

        if (Input.GetKeyDown(KeyCode.T))
        {
            CurrentMode = CameraViewMode.TPS;
            SetStrategy(new TPSCameraStrategy(cam, target, this));
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
