using System;
using UnityEngine;

public class PlaneTouch : MonoCache, IInteracteble
{
    public event Action OnInteract;
    public event Action OnInteractProcess;
    public event Action OnInteractEnd;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Grabbable"))
            OnInteract?.Invoke();
    }
}