using System;
using UnityEngine;

public class TimeManager : MonoBehaviour
{

    [SerializeField]
    [Range(0, 1)]
    private float _time;

    public static float _timeSpeed;

    private static bool _defaultTime = true;

    public static event EventHandler TimeChanged;

    private void Start()
    {
        _timeSpeed = _time;
    }
    public static void SlowDownTime()
    {
        // OnTimeChanged(null, EventArgs.Empty);
        /*  var timeChange = 1 + _timeSpeed;
          TimeScaling.global = timeChange;


  */
        TimeScaling.global = 2;
        // TimeScaling.global = DOTween.To(() => 1, x => timeChange = x, 1f, 0.5f).changeValue;

        //????????
        //    TimeScaling.global = DOVirtual.Float(1, timeChange, 0.5f, null);

    }
    public static void OriginalTime()
    {
        OnTimeChanged(null, EventArgs.Empty);

        TimeScaling.global = 1;
        //TimeScaling.global = DOTween.To(() => 2, x => timeChange = x, 1, 0.5f).changeValue;
    }
    /* public static void ChangeTime()
     {
         if (_defaultTime)
         {
             TimeScaling.global = 1;

         }
         else
         {
             TimeScaling.global = 1 / _timeSpeed;
         }

         _defaultTime = !_defaultTime;
     }*/
    public static void OnTimeChanged(object source, EventArgs eventArgs)
    {
        var handler = TimeChanged;
        handler?.Invoke(null, eventArgs);
    }
    public class TimeScaling
    {
        public static float player = 1;
        public static float global = 1;
    }
}
