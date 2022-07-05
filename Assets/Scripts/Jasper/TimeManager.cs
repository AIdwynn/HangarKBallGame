using UnityEngine;

public class TimeManager : MonoBehaviour
{

    [SerializeField]
    [Range(0, 1)]
    private float _time;

    public static float _timeSpeed;

    private static bool _defaultTime = true;

    private void Start()
    {
        _timeSpeed = _time;
    }
    public static void SlowDownTime()
    {
        var timeChange = 1 + _timeSpeed;
        TimeScaling.global = timeChange;

        // TimeScaling.global = DOTween.To(() => 1, x => timeChange = x, 1f, 0.5f).changeValue;

        //????????
        //    TimeScaling.global = DOVirtual.Float(1, timeChange, 0.5f, null);

    }
    public static void OriginalTime()
    {
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
    public class TimeScaling
    {
        public static float player = 1;
        public static float global = 1;
    }
}
