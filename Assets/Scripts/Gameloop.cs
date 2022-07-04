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
            ballSpawner.BallSpawned += (s, e) => AssignSource(e);
        }
    }
    private void AssignSource(Ball ball)
    {
        ball.AudioManager = _audioManager;
    }

    private void FixedUpdate()
    {
        _audioManager.FixedUpdate();
    }
}
