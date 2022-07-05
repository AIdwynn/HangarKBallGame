using UnityEngine;
using UnityEngine.SceneManagement;

public class Pillar : MonoBehaviour
{
    [SerializeField]
    private LayerMask _ballLayer;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (((1 << collision.gameObject.layer) & _ballLayer) != 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);

        }
    }
}
