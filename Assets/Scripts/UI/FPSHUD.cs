using TMPro;
using UnityEngine;

public class FPSHUD : HUD
{
    [SerializeField] private TMP_Text FPSText;
    private FPSTick FPSTick => FindFirstObjectByType<FPSTick>();    

    private void OnEnable() => FPSTick.OnValueChanged += CalcFPS;
    private void OnDisable() => FPSTick.OnValueChanged -= CalcFPS;
    private void OnDestroy() => FPSTick.OnValueChanged -= CalcFPS;
    private void CalcFPS(float fps) 
    {
        if (FPSText)
            FPSText.text = $"FPS: {Mathf.CeilToInt(fps)}";
        else
            Debug.LogWarning("Object don`t have Component TMP_Text");
    }
}