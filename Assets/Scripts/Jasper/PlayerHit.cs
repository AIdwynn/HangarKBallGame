using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHit : MonoBehaviour
{
    [SerializeField]
    private LayerMask _ballLayer;
    [SerializeField]
    private Player _player;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (((1 << collision.gameObject.layer) & _ballLayer) != 0)
        {
            _player.UnsubscribeAllEvents();
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);


        }

    }


}
