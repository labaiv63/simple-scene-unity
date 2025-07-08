using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class HoldObject: MonoCache, IInteracteble
{
    public event Action OnInteract;
    private Transform draggedObject;
    private Camera cam => Camera.main;
    private Ray ray;
    private Vector3 targetPos;
    private float fixedZ => draggedObject.localPosition.z;
    private Rigidbody rb_draggedObject => GetComponent<Rigidbody>();
    private bool grabbing;
    public override void OnTick() 
    {
        Grab();
    }
    private void Grab()
    {        
        Vector2 screenPos = Pointer.current.position.ReadValue();
        ray = cam.ScreenPointToRay(screenPos);

       
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                if (hit.collider.CompareTag("Grabbable"))
                {
                    draggedObject = hit.collider.transform;
                }
            }
        }
        
        if (Mouse.current.leftButton.isPressed && draggedObject != null)
        {            
            Vector3 screenWithZ = new Vector3(screenPos.x, screenPos.y, fixedZ);
            targetPos = cam.ScreenToWorldPoint(screenWithZ);

            Vector3 forceDir = (targetPos - rb_draggedObject.position) * 10f;
            float maxSpeed = 10f;
            rb_draggedObject.linearVelocity = Vector3.ClampMagnitude(forceDir, maxSpeed);
            if (!grabbing)
            {
                grabbing = true;
                OnInteract?.Invoke();
            } 
            
        }

        if (Mouse.current.leftButton.wasReleasedThisFrame)
        {
            draggedObject = null;
            grabbing = false;
        }
    }
}