using System.Collections;
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

            StartCoroutine(WaitASEC());
            //StartCoroutine(WaitASEC());

        }

    }

    private IEnumerator WaitASEC()
    {
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

    }
}
