using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//public class ColorChangeScript : MonoBehaviour
//{
//    public AudioManager AudioManager;
//    public List<Color> Colors;
//    private Material _shaderMat;
//    private int _fadeSpeed = 3;
//    private int _colorIndex = 0;

//    private Color prevColor;


//    private void Start()
//    {
//        AudioManager.boopHandler += (s, e) =>
//        {
//            StartCoroutine(ChangeColor(Time.fixedDeltaTime));
//        };
//        prevColor = Colors[0];
//        _shaderMat = this.GetComponent<SpriteRenderer>().material;
//    }

//    private IEnumerator ChangeColor(float deltatime)
//    {
//        _colorIndex++;
//        if (_colorIndex == Colors.Count) _colorIndex = 0;
//        Color newColor = Colors[_colorIndex];
//        _shaderMat.SetColor("_startColor", prevColor);
//        _shaderMat.SetColor("_endColor", newColor);
//        _shaderMat.SetFloat("_fade", 0);
//        float fadeProgression = 0;

//        while (fadeProgression < 1)
//        {
//            fadeProgression += _fadeSpeed * deltatime;
//            _shaderMat.SetFloat("_fade", fadeProgression);
//            yield return new WaitForFixedUpdate();
//        }
//        _shaderMat.SetFloat("_fade", 1);
//        prevColor = newColor;
//    }
//}
