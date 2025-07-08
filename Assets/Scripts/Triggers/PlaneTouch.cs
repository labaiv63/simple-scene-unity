using System;
using UnityEngine;

public class PlaneTouch : MonoCache, IInteracteble
{
    public event Action OnInteract;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Grabbable"))
            OnInteract?.Invoke();
    }
}