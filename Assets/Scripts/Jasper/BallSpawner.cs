using System;
using UnityEngine;

public class BallSpawner : MonoBehaviour
{
    internal System.Func<object, object, object> BallSpawned;
    [SerializeField]
    private GameObject _ball;

    [SerializeField]
    private Transform _ballTransformSpawnPoint;

    [SerializeField]
    private float _timeToSpawnBall;

    private event EventHandler BallSpawned;

    private float _timer;

    private void FixedUpdate()
    {
        _timer += Time.deltaTime;

        if (_timer >= _timeToSpawnBall)
        {
            _timer = 0;
            Instantiate(_ball, _ballTransformSpawnPoint);
        }
    }

    public void OnBallSpawmed(object source, EventArgs eventArgs)
    {
        var handler = BallSpawned;
        handler?.Invoke(this, eventArgs);
    }
}
