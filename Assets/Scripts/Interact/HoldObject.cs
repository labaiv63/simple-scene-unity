using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class HoldObject: MonoCache, IInteracteble
{
    public event Action OnInteract;
    public event Action OnInteractProcess;
    public event Action OnInteractEnd;
    private Transform draggedObject;
    private Camera cam => Camera.main;
    private Ray ray;
    private Vector3 targetPos;
    private float fixedZ => draggedObject.localPosition.z;
    private Rigidbody rb_draggedObject;
    private bool isGrabbing;

    private PlayerControll playerControll;
    
    public override void OnTick() 
    {
        if (playerControll == null)
            playerControll = FindFirstObjectByType<PlayerControll>();
    }
    private void OnEnable()
    {
        playerControll = FindFirstObjectByType<PlayerControll>();

        if (playerControll != null)
        {
            playerControll.OnGrabStarted += HandleGrabStart;
            playerControll.OnGrabHeld += HandleGrabHeld;
            playerControll.OnGrabReleased += HandleGrabReleased;
        }
    }
    private void OnDestroy()
    {
        if (playerControll != null)
        {
            playerControll.OnGrabStarted -= HandleGrabStart;
            playerControll.OnGrabHeld -= HandleGrabHeld;
            playerControll.OnGrabReleased -= HandleGrabReleased;
        }
    }
    private void OnDisable()
    {
        if (playerControll != null)
        {
            playerControll.OnGrabStarted -= HandleGrabStart;
            playerControll.OnGrabHeld -= HandleGrabHeld;
            playerControll.OnGrabReleased -= HandleGrabReleased;
        }
    }
    private void HandleGrabStart() 
    {
        Vector2 screenPos = Pointer.current.position.ReadValue();
        ray = cam.ScreenPointToRay(screenPos);

        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            if (hit.collider.CompareTag("Grabbable"))
            {
                draggedObject = hit.collider.transform;
                rb_draggedObject = draggedObject.GetComponent<Rigidbody>();

                if (rb_draggedObject != null)
                {
                    rb_draggedObject.useGravity = false;
                    isGrabbing = true;
                    OnInteract?.Invoke();
                }
            }
        }
    }
    private void HandleGrabHeld()
    {
        if (draggedObject == null || rb_draggedObject == null)
            return;

        Vector2 screenPos = Pointer.current.position.ReadValue();
        Vector3 screenWithZ = new Vector3(screenPos.x, screenPos.y, fixedZ);
        targetPos = cam.ScreenToWorldPoint(screenWithZ);

        Vector3 forceDir = (targetPos - rb_draggedObject.position) * 10f;
        float maxSpeed = 10f;
        rb_draggedObject.linearVelocity = Vector3.ClampMagnitude(forceDir, maxSpeed);
        OnInteractProcess?.Invoke();
    }
    private void HandleGrabReleased()
    {
        if (rb_draggedObject != null)
        {
            rb_draggedObject.useGravity = true;
            rb_draggedObject = null;
        }

        draggedObject = null;
        isGrabbing = false;
        OnInteractEnd?.Invoke();
    }
}