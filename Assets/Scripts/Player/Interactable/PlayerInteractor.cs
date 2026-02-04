using UnityEngine;

public class PlayerInteractor : MonoBehaviour
{
    public Camera cam;
    public float range = 3f;
    public LayerMask interactMask;

    public IInteractable Current { get; private set; }

    void Update()
    {
        Current = null;

        Ray ray = new Ray(cam.transform.position, cam.transform.forward);
        if (Physics.Raycast(ray, out RaycastHit hit, range, interactMask))
        {
            Current = hit.collider.GetComponentInParent<IInteractable>();
        }

        if (Current != null && Input.GetKeyDown(KeyCode.E))
        {
            Current.Interact(gameObject);
        }

        // þimdilik prompt'u console'a basalým
        if (Current != null)
            Debug.Log(Current.GetPrompt());
    }
}
