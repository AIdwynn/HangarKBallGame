using UnityEngine;

public class Reflector : MonoBehaviour
{
    [SerializeField]
    private LayerMask _ballLayer;

    [SerializeField]
    private Player _player;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (((1 << collision.gameObject.layer) & _ballLayer) != 0)
        {
            collision.gameObject.GetComponent<Ball>().ReflectBall(collision);
            _player.Pushback(collision);
        }
    }


}
