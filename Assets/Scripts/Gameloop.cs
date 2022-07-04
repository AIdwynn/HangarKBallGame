using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gameloop : MonoBehaviour
{
    private AudioManager _audioManager;

    [SerializeField] private int bpm;
    [SerializeField] private AudioClip _beepSound;
    [SerializeField] private AudioClip _boopSound;
    [SerializeField] private AudioSource _auidioSource;

    [SerializeField] private List<BallSpawner> _ballSpawners;


    void Start()
    {
        _audioManager = new AudioManager(bpm, _beepSound, _boopSound, _auidioSource);
        foreach (var ballSpawner in _ballSpawners)
        {
            ballSpawner.BallSpawned += (s, e) => AssignSource(e.Ball);
        }
    }
    private void AssignSource(GameObject ball)
    {
        _audioManager.IsBall = true;
        ball.AddComponent<ColorChangeScript>();
        ball.GetComponent<ColorChangeScript>().AudioManager = _audioManager;
    }

    private void FixedUpdate()
    {
        _audioManager.FixedUpdate();
    }
}
