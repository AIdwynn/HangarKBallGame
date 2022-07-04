using UnityEngine;
using UnityEngine.SceneManagement;

public class Ball : MonoBehaviour
{
    [SerializeField]
    private Rigidbody2D _rb;

    public float _ballSpeed;

    [SerializeField]
    private LayerMask _playerLayer;

    [SerializeField]
    private LayerMask _reflectLayer;

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
        _rb.AddForce(_direction * _ballSpeed * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (((1 << collision.gameObject.layer) & _playerLayer) != 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        if (((1 << collision.gameObject.layer) & _reflectLayer) != 0)
        {
            var mult = _lastVelocity.magnitude;
            var inDirection = _lastVelocity.normalized;

            _rb.velocity = Vector2.Reflect(inDirection, collision.contacts[0].normal.normalized);
        }
    }



}
