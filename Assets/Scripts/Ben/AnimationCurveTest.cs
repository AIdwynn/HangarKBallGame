using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationCurveTest : MonoBehaviour
{
    public AnimationCurve curve;

    void Start()
    {
     
    }

    // Update is called once per frame
    void Update()
    {


        Debug.Log(curve.Evaluate(0.4f));
    }
}
