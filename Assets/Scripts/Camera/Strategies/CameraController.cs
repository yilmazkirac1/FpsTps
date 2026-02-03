using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;
    public Camera cam;

    [Header("Sensitivity Settings")]
    public float fpsSensitivity = 150f;
    public float tpsSensitivity = 120f;

    private ICameraStrategy currentStrategy;

    void Start()
    {
        SetStrategy(new TPSCameraStrategy(cam, target, this));
    }

    void Update()
    {
        currentStrategy?.UpdateCamera();

        if (Input.GetKeyDown(KeyCode.F))
            SetStrategy(new FPSCameraStrategy(cam, target, this));

        if (Input.GetKeyDown(KeyCode.T))
            SetStrategy(new TPSCameraStrategy(cam, target, this));
    }

    public void SetStrategy(ICameraStrategy newStrategy)
    {
        currentStrategy?.OnExit();
        currentStrategy = newStrategy;
        currentStrategy.OnEnter();
    }
}
