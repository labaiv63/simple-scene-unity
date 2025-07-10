using UnityEngine;
using UnityEngine.InputSystem;
using System;

public class PlayerControll : MonoCache
{
    public event Action OnGrabStarted;
    public event Action OnGrabHeld;
    public event Action OnGrabReleased;
    public override void OnTick()
    {
        ControllCheck();
    }
    private void ControllCheck() 
    {
        if (Mouse.current.leftButton.wasPressedThisFrame)
            OnGrabStarted?.Invoke();

        if (Mouse.current.leftButton.isPressed)
            OnGrabHeld?.Invoke();

        if (Mouse.current.leftButton.wasReleasedThisFrame)
            OnGrabReleased?.Invoke();
    }
}