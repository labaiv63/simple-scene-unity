using System.Collections.Generic;
using UnityEngine;

public class MonoCache : MonoBehaviour
{
    public static List<MonoCache> allUpdate = new List<MonoCache>(10001);
    public static List<MonoCache> allFixedUpdate = new List<MonoCache>(10001);

    private void OnEnable() => allUpdate.Add(this);
    private void OnDisable() => allUpdate.Remove(this);
    private void OnDestroy() => allUpdate.Remove(this);
    protected void AddFixedUpdate() { }
    protected void RemoveFixedUpdate() { }
    public void Tick() => OnTick();    
    public void FixedTick() => OnFixedTick();
    public virtual void OnTick() { }
    public virtual void OnFixedTick() { }
}