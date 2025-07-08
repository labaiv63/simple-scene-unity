using System.Collections.Generic;
using UnityEngine;

public class GlobalUpdater : MonoBehaviour
{
    private void Update()
    {
        for (int i = 0; i<MonoCache.allUpdate.Count; i++)
            MonoCache.allUpdate[i].Tick();
    }
    private void FixedUpdate()
    {
        for (int i = 0; i < MonoCache.allFixedUpdate.Count; i++)
            MonoCache.allFixedUpdate[i].FixedTick();
    }
}