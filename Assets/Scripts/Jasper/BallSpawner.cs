using System;
using UnityEngine;

public class BallEventArgs : EventArgs
{
    public GameObject Ball;

    public BallEventArgs(GameObject ball)
    {
        Ball = ball;
    }
}
public class BallSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject _ball;

    [SerializeField]
    private Transform _ballTransformSpawnPoint;

    [SerializeField]
    private float _timeToSpawnBall;

    public event EventHandler<BallEventArgs> BallSpawned;

    private float _timer;

    private bool _oneSpawned = false;
    private void FixedUpdate()
    {
        if (!_oneSpawned)
        {
            _timer += Time.deltaTime / TimeManager.TimeScaling.global;

            if (_timer >= _timeToSpawnBall)
            {
                _timer = 0;
                var ball = Instantiate(_ball, _ballTransformSpawnPoint.position, Quaternion.identity, null);
                OnBallSpawmed(this, new BallEventArgs(ball));
                _oneSpawned = true;

            }
        }

    }

    public void OnBallSpawmed(object source, BallEventArgs eventArgs)
    {
        var handler = BallSpawned;
        handler?.Invoke(this, eventArgs);
    }
}
