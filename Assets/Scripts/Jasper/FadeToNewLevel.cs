using UnityEngine;

public class FadeToNewLevel : MonoBehaviour
{
    [SerializeField]
    private GameObject _fadeCanvas;

    [SerializeField]
    private LayerMask _playerLayer;

    [SerializeField]
    private Player _player;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (((1 << collision.gameObject.layer) & _playerLayer) != 0)
        {
            _player.UnsubscribeAllEvents();
            _fadeCanvas.SetActive(true);


        }
    }
}
