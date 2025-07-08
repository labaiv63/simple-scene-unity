using System;
using UnityEngine;

public class FPSTick : MonoCache
{
    public event Action<float> OnValueChanged;    
    private float deltaTime;

    private float fps;
    public float FPS
    {
        get => fps;
        set
        {
            fps = value;
            OnValueChanged?.Invoke(fps);
        }
    }
    public override void OnTick()
    {
        deltaTime += (Time.unscaledDeltaTime - deltaTime) * 0.1f;
        FPS = 1.0f / deltaTime;
    }
}