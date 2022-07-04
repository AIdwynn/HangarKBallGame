using UnityEngine;

public class BallSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject _ball;

    [SerializeField]
    private Transform _ballTransformSpawnPoint;

    [SerializeField]
    private float _timeToSpawnBall;

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
}
