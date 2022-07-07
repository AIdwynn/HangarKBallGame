using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class PostprocessingManager : MonoBehaviour
{
    public Volume Volume;
    ColorAdjustments colorAdjustments;
    bool canExecuteColorAdjustments = false;

    // Start is called before the first frame update
    void Start()
    {
        if (Volume.profile.TryGet<ColorAdjustments>(out colorAdjustments))
        {
            canExecuteColorAdjustments = true;
        }
    }

   public void StartHueChange()
    {
        StartCoroutine(HueShift(1f));
    }

    public void StopHueChange()
    {
        StopCoroutine(HueShift(1f));
    }

    IEnumerator HueShift(float speedPerFrame)
    {
        while (canExecuteColorAdjustments)
        {
            colorAdjustments.hueShift.value += 0.1f;
            if (colorAdjustments.hueShift.value >= 180)
            {
                colorAdjustments.hueShift.value = -180;
            }
            yield return new WaitForEndOfFrame();
        }

      
    }
}
