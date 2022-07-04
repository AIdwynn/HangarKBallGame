using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorChangeScript : MonoBehaviour
{
    public AudioManager AudioManager;
    public List<Color> Colors;
    private int colorIndex;

    private void Start()
    {
        AudioManager.boopHandler += (s, e) =>
        {

        };
    }
}
