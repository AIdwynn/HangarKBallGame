using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileMapManager : MonoBehaviour
{
    //[SerializeField] private List<Tilemap> _walls;

    //[SerializeField] private List<Tilemap> _ground;

    //[SerializeField] private List<Tilemap> _decals;

    private float DurationMult = 1;

    private bool FlashTiles = true;

    public List<MapComponent> testMapComponent;




    private void Start()
    {        

        for (int i = 1; i < testMapComponent.Count; i++)
        {
            Color.RGBToHSV(testMapComponent[i].startColor, out float H1, out float S1, out float V1);
            testMapComponent[i].StartHue = H1;
            testMapComponent[i].StartSaturation = S1;
            testMapComponent[i].StartValue = V1;

            Color.RGBToHSV(testMapComponent[i].EndColor, out float H2, out float S2, out float V2);

            testMapComponent[i].TargetHue = H2;
            testMapComponent[i].TargetSaturation = S2;
            testMapComponent[i].TargetValue = V2;

            if (testMapComponent[i].AnimationType == FlashingType.Lerp)
            {
                StartCoroutine(FlashTileLoopValue(testMapComponent[i].ValueDuration, testMapComponent[i]));
                StartCoroutine(FlashTileLoopSaturation(testMapComponent[i].SaturationDuration, testMapComponent[i]));
                StartCoroutine(FlashTileLoopHue(testMapComponent[i].SaturationDuration, testMapComponent[i]));
            }
            else if (testMapComponent[i].AnimationType == FlashingType.LerpDrop)
            {
                StartCoroutine(FlashTileLoopValueDrop(testMapComponent[i].ValueDuration, testMapComponent[i]));
                StartCoroutine(FlashTileLoopSaturationDrop(testMapComponent[i].SaturationDuration, testMapComponent[i]));
                StartCoroutine(FlashTileLoopHueDrop(testMapComponent[i].SaturationDuration, testMapComponent[i]));
            }


        }
    }

    private void Update()
    {
        for (int i = 1; i < testMapComponent.Count; i++)
        {
            //Color.RGBToHSV(testMapComponent[i].map.color, out float H, out float S, out float V);
            //testMapComponent[i].StartHue = H;
            //testMapComponent[i].StartSaturation = H;
            //testMapComponent[i].StartValue = H;

            ApplyMapColour(testMapComponent[i]);
        }


    }   

    public void ChangeSpeed(float BPM)
    {
       DurationMult = 60 / BPM; 
    }


    // lerp
    IEnumerator FlashTileLoopHue(float duration, MapComponent mapcomponent)
    {
        while (FlashTiles)
        {
            StartCoroutine(AnimateTileHue(duration / 2, mapcomponent));

            yield return new WaitForSeconds((duration * DurationMult));
        }
    }
    IEnumerator FlashTileLoopSaturation(float duration, MapComponent mapcomponent)
    {
        while (FlashTiles)
        {
            yield return StartCoroutine(AnimateTileSaturation(duration / 2, mapcomponent));

            //yield return new WaitForSeconds((duration * DurationMult));
        }
    }

    IEnumerator FlashTileLoopValue(float duration, MapComponent mapcomponent)
    {
        while (FlashTiles)
        {
            yield return StartCoroutine(AnimateTileValue(duration / 2, mapcomponent));

            //yield return new WaitForSeconds((duration * DurationMult));
        }
    }

    //dropRoutineStarters
    IEnumerator FlashTileLoopHueDrop(float duration, MapComponent mapcomponent)
    {
        while (FlashTiles)
        {
            yield return StartCoroutine(AnimateTileHueDrop(duration, mapcomponent));

            //yield return new WaitForSeconds((duration * DurationMult));
        }
    }
    IEnumerator FlashTileLoopSaturationDrop(float duration, MapComponent mapcomponent)
    {
        while (FlashTiles)
        {
            yield return StartCoroutine(AnimateTileSaturationDrop(duration, mapcomponent));

            //yield return new WaitForSeconds((duration * DurationMult));
        }
    }

    IEnumerator FlashTileLoopValueDrop(float duration, MapComponent mapcomponent)
    {
        while (FlashTiles)
        {
            yield return StartCoroutine(AnimateTileValueDrop(duration, mapcomponent));

            //yield return new WaitForSeconds((duration * DurationMult));
        }
    }

    //

    // drop off

    IEnumerator AnimateTileHueDrop(float duration, MapComponent MapComponent)
    {
        float lastTime = Time.realtimeSinceStartup;
        float timer = 0;

        //float TargetHue = Hue + HueShift;
        float targetSat = MapComponent.TargetHue;
        if (targetSat > 1)
        {
            targetSat = 1;
        }

        MapComponent.ChangedHue = MapComponent.StartHue;

        while (timer < (duration * DurationMult))
        {
            MapComponent.ChangedHue = Mathf.Lerp(MapComponent.StartHue, targetSat, timer / (duration * DurationMult));

            timer += (Time.realtimeSinceStartup - lastTime);
            lastTime = Time.realtimeSinceStartup;

            yield return null;

        }

        MapComponent.ChangedHue = MapComponent.StartHue;
    }
    IEnumerator AnimateTileSaturationDrop(float duration, MapComponent MapComponent)
    {
        float lastTime = Time.realtimeSinceStartup;
        float timer = 0;

        //float TargetHue = Hue + HueShift;
        float targetSat = MapComponent.TargetSaturation;
        if (targetSat > 1)
        {
            targetSat = 1;
        }

        MapComponent.ChangedSaturation = MapComponent.StartSaturation;

        while (timer < (duration * DurationMult))
        {
            MapComponent.ChangedSaturation = Mathf.Lerp(MapComponent.StartSaturation, targetSat, timer / (duration * DurationMult));

            timer += (Time.realtimeSinceStartup - lastTime);
            lastTime = Time.realtimeSinceStartup;

            yield return null;

        }
        MapComponent.ChangedSaturation = MapComponent.StartSaturation;
    }
    IEnumerator AnimateTileValueDrop(float duration, MapComponent MapComponent)
    {
        float lastTime = Time.realtimeSinceStartup;
        float timer = 0;

        //float TargetHue = Hue + HueShift;
        float targetValue = MapComponent.TargetValue;
        if (targetValue > 1)
        {
            targetValue = 1;
        }

        MapComponent.ChangedValue = MapComponent.StartValue;

        while (timer < (duration * DurationMult))
        {

            MapComponent.ChangedValue = Mathf.Lerp(MapComponent.StartValue, targetValue, timer / (duration * DurationMult));

            timer += (Time.realtimeSinceStartup - lastTime);
            lastTime = Time.realtimeSinceStartup;

            yield return null;
        }
        MapComponent.ChangedValue = MapComponent.StartValue;
    }
    //flashingRoutines

    IEnumerator AnimateTileHue(float duration, MapComponent MapComponent)
    {
        float lastTime = Time.realtimeSinceStartup;
        float timer = 0;

        //float TargetHue = Hue + HueShift;
        float targetSat = MapComponent.TargetHue;
        if (targetSat > 1)
        {
            targetSat = 1;
        }

        MapComponent.ChangedHue = MapComponent.StartHue;

        while (timer < (duration * DurationMult))
        {
            MapComponent.ChangedHue = Mathf.Lerp(MapComponent.StartHue, targetSat, timer / (duration * DurationMult));

            timer += (Time.realtimeSinceStartup - lastTime);
            lastTime = Time.realtimeSinceStartup;

            yield return null;

        }
        StartCoroutine(AnimateTileHueDown(duration, MapComponent));
    }

    IEnumerator AnimateTileHueDown(float duration, MapComponent MapComponent)
    {
        float lastTime = Time.realtimeSinceStartup;
        float timer = 0;

        //float TargetHue = Hue + HueShift;
        float currentValue = MapComponent.ChangedHue;
        float targetValue = MapComponent.StartHue;

        while (timer < (duration * DurationMult))
        {
            MapComponent.ChangedHue = Mathf.Lerp(currentValue, targetValue, timer / (duration * DurationMult));

            timer += (Time.realtimeSinceStartup - lastTime);
            lastTime = Time.realtimeSinceStartup;

            yield return null;
        }

    }

    IEnumerator AnimateTileSaturation(float duration, MapComponent MapComponent)
    {
        float lastTime = Time.realtimeSinceStartup;
        float timer = 0;

        //float TargetHue = Hue + HueShift;
        float targetSat = MapComponent.TargetSaturation;
        if (targetSat > 1)
        {
            targetSat = 1;
        }

        MapComponent.ChangedSaturation = MapComponent.StartSaturation;

        while (timer < (duration * DurationMult))
        {
            MapComponent.ChangedSaturation = Mathf.Lerp(MapComponent.StartSaturation, targetSat, timer / (duration * DurationMult));

            timer += (Time.realtimeSinceStartup - lastTime);
            lastTime = Time.realtimeSinceStartup;

            yield return null;

        }
        StartCoroutine(AnimateTileStaturationDown(duration, MapComponent));
    }

    IEnumerator AnimateTileStaturationDown(float duration, MapComponent MapComponent)
    {
        float lastTime = Time.realtimeSinceStartup;
        float timer = 0;

        //float TargetHue = Hue + HueShift;
        float currentValue = MapComponent.ChangedSaturation;
        float targetValue = MapComponent.StartSaturation;

        while (timer < (duration * DurationMult))
        {
            MapComponent.ChangedSaturation = Mathf.Lerp(currentValue, targetValue, timer / (duration * DurationMult));

            timer += (Time.realtimeSinceStartup - lastTime);
            lastTime = Time.realtimeSinceStartup;

            yield return null;
        }

    }

    IEnumerator AnimateTileValue(float duration, MapComponent MapComponent)
    {
        float lastTime = Time.realtimeSinceStartup;
        float timer = 0;

        //float TargetHue = Hue + HueShift;
        float targetValue = MapComponent.TargetValue;
        if (targetValue > 1)
        {
            targetValue = 1;
        }

        MapComponent.ChangedValue = MapComponent.StartValue;

        while (timer < (duration * DurationMult))
        {

            MapComponent.ChangedValue = Mathf.Lerp(MapComponent.StartValue, targetValue, timer / (duration * DurationMult));

            timer += (Time.realtimeSinceStartup - lastTime);
            lastTime = Time.realtimeSinceStartup;

            yield return null;
        }
        StartCoroutine(AnimateTileValueDown(duration, MapComponent));
    }

    IEnumerator AnimateTileValueDown(float duration, MapComponent MapComponent)
    {
        float lastTime = Time.realtimeSinceStartup;
        float timer = 0;

        //float TargetHue = Hue + HueShift;
        float currentValue = MapComponent.ChangedValue;
        float targetValue = MapComponent.StartValue;

        while (timer < (duration * DurationMult))
        {
            MapComponent.ChangedValue = Mathf.Lerp(currentValue, targetValue, timer / (duration * DurationMult));

            timer += (Time.realtimeSinceStartup - lastTime);
            lastTime = Time.realtimeSinceStartup;


            yield return null;
        }

    }

    public void ApplyMapColour(MapComponent MapComponent)
    {
        MapComponent.map.color = Color.HSVToRGB(MapComponent.ChangedHue, MapComponent.ChangedSaturation, MapComponent.ChangedValue);
    }

}


[System.Serializable]
public class MapComponent
{
    public FlashingType AnimationType;

    public Color startColor;

    public Color EndColor;

    [HideInInspector]
    public float StartHue, StartSaturation, StartValue;

    [HideInInspector]
    public float TargetHue, TargetSaturation, TargetValue;

    [HideInInspector]
    public float ChangedHue, ChangedSaturation, ChangedValue;

    public float Hueduration;

    public float SaturationDuration;

    public float ValueDuration;

    public Tilemap map;
}

public enum FlashingType
{
    Lerp,
    LerpDrop
}