using UnityEngine;

public class InputManager : MonoBehaviour
{
    [SerializeField] private BasicTurret[] turrets;
    [SerializeField] private Transform ground;

    private Camera cam;
    private Plane groundPlane;

    private void Awake()
    {
        cam = Camera.main;
        if (cam == null)
            Debug.LogError("No hay Main Camera asignada.");
    }

    private void Update()
    {
        if (GameManager.I != null && GameManager.I.IsGameOver) return;
        if (ground == null || turrets == null || turrets.Length < 3) return;

        groundPlane = new Plane(Vector3.up, ground.position);

        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            if (groundPlane.Raycast(ray, out float enter))
            {
                Vector3 worldPoint = ray.GetPoint(enter);
                int sector = GetSector(Input.mousePosition.x, Screen.width);

                turrets[sector].SendMessage("HandlePlayerClick", worldPoint, SendMessageOptions.DontRequireReceiver);
            }
        }
    }

    private int GetSector(float clickX, float screenWidth)
    {
        Vector3 mouse=cam.ScreenPointToRay(new Vector3(clickX, 0)).GetPoint(25);
        clickX = mouse.x;
        Debug.Log((clickX));
        if (clickX < -3.33f) return 0;
        if (clickX < 3.33) return 1;
        return 2;
    }
}
