using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Fader : MonoBehaviour
{

    // the image you want to fade, assign in inspector
    [SerializeField]
    private Image _img;

    [SerializeField]
    private float _speed;

    private void Start()
    {
        FadeToBlack();
    }

    public void FadeToTransparent()
    {
        StartCoroutine(FadeImage(true, _speed));
    }

    public void FadeToBlack()
    {
        StartCoroutine(FadeImage(false, _speed));
    }



    IEnumerator FadeImage(bool fadeAway, float speed)
    {
        // fade from opaque to transparent
        if (fadeAway)
        {
            // loop over 1 second backwards
            for (float i = 1; i > 0; i -= Time.deltaTime * speed)
            {
                // set color with i as alpha
                _img.color = new Color(0, 0, 0, i);
                yield return null;
            }
        }
        // fade from transparent to opaque
        else
        {
            // loop over 1 second
            for (float i = 0; i <= 1; i += Time.deltaTime * speed)
            {
                // set color with i as alpha
                _img.color = new Color(0, 0, 0, i);
                yield return null;
            }
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

        }
    }
}
