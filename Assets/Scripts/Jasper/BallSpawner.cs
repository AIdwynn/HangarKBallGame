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

    private void FixedUpdate()
    {
        _timer += Time.deltaTime;

        if (_timer >= _timeToSpawnBall)
        {
            _timer = 0;
            var ball = Instantiate(_ball, _ballTransformSpawnPoint);
            OnBallSpawmed(this, new BallEventArgs(ball));
        }
    }

    public void OnBallSpawmed(object source, BallEventArgs eventArgs)
    {
        var handler = BallSpawned;
        handler?.Invoke(this, eventArgs);
    }
}
