using System;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField]
    private Rigidbody2D _rb;

    public float _ballSpeed;

    [SerializeField]
    private float _ballSpeedUpValue;

    [SerializeField]
    private float _maxVelocity;

    public event EventHandler BallBounced;

    public AudioManager _audioManager;

    private Vector2 _lastVelocity;

    private Vector2 _direction;


    private void Start()
    {
        _direction = transform.right;
        _rb = gameObject.GetComponent<Rigidbody2D>();
        ShootBall();
        TimeManager.TimeSlow += BallSlowDown;
        TimeManager.TimeOriginal += BallSpeedUp;
        // TimeManager.TimeChanged += TimeVelocity;
    }

    private void FixedUpdate()
    {
        //  _rb.velocity /= TimeManager.TimeScaling.global;

        _rb.velocity = (_direction * _ballSpeed * Time.deltaTime) / TimeManager.TimeScaling.global;
        _lastVelocity = _rb.velocity;
        // Debug.Log(_direction);
        //  _rb.velocity /
    }

    private void BallSlowDown(object source, EventArgs eventArgs)
    {
        /*  if (_rb != null)
              _rb.velocity /= TimeManager.TimeScaling.global;*/

    }
    private void BallSpeedUp(object source, EventArgs eventArgs)
    {
        /*  if (_rb != null)
              _rb.velocity *= TimeManager.TimeScaling.global;
  */
    }

    private void ShootBall()
    {
        // _rb.AddForce((_direction * _ballSpeed * Time.deltaTime) * _rb.mass, ForceMode2D.Impulse);
    }

    public void ReflectBall(Collision2D collision)
    {
        OnBallBounced(this, EventArgs.Empty);

        /*  ContactPoint2D cp = collision.contacts[0];
          _lastVelocity = Vector3.Reflect(_lastVelocity, cp.normal);
          _direction = _lastVelocity;*/
        //  SetVelocity();





        // _ballSpeed += _ballSpeedUpValue;

        var mult = _lastVelocity.magnitude;
        var inDirection = _lastVelocity.normalized;

        _direction = Vector2.Reflect(inDirection, collision.contacts[0].normal.normalized);
    }

    private void SetVelocity()
    {
        _rb.velocity = _lastVelocity;

        /* if (_rb.velocity.x >= _maxVelocity || _rb.velocity.x <= -_maxVelocity || _rb.velocity.y >= _maxVelocity || _rb.velocity.y <= -_maxVelocity)
         {
             Debug.Log("max Reached");
         }
         else
         {
             _rb.velocity = _lastVelocity * _ballSpeedUpValue;

         }*/
    }

    public void OnBallBounced(object source, EventArgs eventArgs)
    {
        var handler = BallBounced;
        handler?.Invoke(this, eventArgs);
    }



}
