using System;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField]
    private Rigidbody2D _rb;

    public float _ballSpeed;



    public event EventHandler BallBounced;

    public AudioManager _audioManager;

    private Vector2 _lastVelocity;

    private Vector2 _direction;

    private void Start()
    {
        _direction = Vector2.right;
    }

    private void FixedUpdate()
    {
        MoveBall();
        _lastVelocity = _rb.velocity;
    }

    private void MoveBall()
    {
        _rb.AddForce((_direction * _ballSpeed * Time.deltaTime) * _rb.mass);
        _rb.velocity /= TimeManager.TimeScaling.global;
    }

    public void ReflectBall(Collision2D collision)
    {
        OnBallBounced(this, EventArgs.Empty);

        ContactPoint2D cp = collision.contacts[0];
        _lastVelocity = Vector3.Reflect(_lastVelocity, cp.normal);
        _rb.velocity = _lastVelocity;

        /*
                var mult = _lastVelocity.magnitude;
                var inDirection = _lastVelocity.normalized;

                if (mult < 25)
                    _rb.velocity = Vector2.Reflect(inDirection, collision.contacts[0].normal.normalized) * (25 * 1.4f);*/
    }

    public void OnBallBounced(object source, EventArgs eventArgs)
    {
        var handler = BallBounced;
        handler?.Invoke(this, eventArgs);
    }



}
