using UnityEngine;

public class Reflector : MonoBehaviour
{
    [SerializeField]
    private LayerMask _ballLayer;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (((1 << collision.gameObject.layer) & _ballLayer) != 0)
        {
            Debug.Log("lol");
            collision.gameObject.GetComponent<Ball>().ReflectBall(collision);
        }
    }
}
