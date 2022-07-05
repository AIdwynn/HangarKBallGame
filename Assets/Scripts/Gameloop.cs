using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gameloop : MonoBehaviour
{
    private AudioManager _audioManager;
    [SerializeField] private List<Color> _colors;


    [SerializeField] private AudioClip _beepSound;
    [SerializeField] private AudioClip _boopSound;
    [SerializeField] private AudioSource _auidioSource;
    [SerializeField] private SongNames _songName;
    [SerializeField] private MusicData _musicData;
    [SerializeField] private AudioSource _musicSource;

    [SerializeField] private List<BallSpawner> _ballSpawners;

    private void Awake()
    {
        _audioManager = new AudioManager(_beepSound, _boopSound, _auidioSource, _songName, _musicData, _musicSource);
    }
    void Start()
    {
        foreach (var ballSpawner in _ballSpawners)
        {
            ballSpawner.BallSpawned += (s, e) => AssignSource(e.Ball);
        }
    }
    private void AssignSource(GameObject ball)
    {
        _audioManager.IsBall = true;
        
        var script = ball.AddComponent<ColorChangeScript>();
        script.AudioManager = _audioManager;
        script.Colors = _colors;
        var script2 = ball.GetComponent<Ball>();
        script2.BallBounced += (s,e) => { _audioManager.SpeedIncrease(); };
    }

    private void FixedUpdate()
    {
        _audioManager.FixedUpdate();
    }
}
