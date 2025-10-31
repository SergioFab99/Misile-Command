using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class InputManager : MonoBehaviour
{
    private Camera cam;

    private void Awake()
    {
        cam = Camera.main;
        if (cam == null)
            Debug.LogError("no hay main camera chistoso");
    }

    private void Update()
    {
        if (GameManager.I != null && GameManager.I.IsGameOver) return;

        if (Input.GetMouseButtonDown(0))
        {
            if (cam == null) return;

            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                EventBus.I?.PlayerClicked(hit.point);
            }
        }
    }
}
