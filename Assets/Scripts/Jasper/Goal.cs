using UnityEngine;

public class Goal : MonoBehaviour
{
    [SerializeField]
    private LayerMask _ballLayer;

    [SerializeField]
    private GameObject _plate;

    [SerializeField]
    private GameObject _door;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (((1 << collision.gameObject.layer) & _ballLayer) != 0)
        {
            //  LevelManager.LoadLevel(SceneManager.GetActiveScene().buildIndex + 1);
            Destroy(collision.gameObject);
            _plate.SetActive(true);
            _door.SetActive(false);
        }
    }
}
