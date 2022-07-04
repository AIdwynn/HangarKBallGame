using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField]
    private Rigidbody2D _rb;

    public float _ballSpeed;

    [SerializeField]
    private LayerMask _playerLayer;

    private void FixedUpdate()
    {
        MoveBall();
    }

    private void MoveBall()
    {
        _rb.AddForce(Vector2.right * _ballSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

    }


}
