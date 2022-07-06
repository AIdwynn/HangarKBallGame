using System.Collections.Generic;
using UnityEngine;

public class Gameloop : MonoBehaviour
{

    [Header("Gamedesign Variables")]
    [SerializeField] private float _pitchIncrease;
    [SerializeField] private float _timeSlowAmount;
    [SerializeField] private List<Color> _colors;
    [SerializeField] private AudioClip _beepSound;
    [SerializeField] private AudioClip _boopSound;
    [SerializeField] private SongNames _songName;
    [SerializeField] private List<BallSpawner> _ballSpawners;


    [Header("Unchangeable Values")]
    [SerializeField] private AudioSource _auidioSource;
    [SerializeField] private MusicData _musicData;
    [SerializeField] private AudioSource _musicSource;
    [SerializeField] private TimeManager _timeManager;
    private AudioManager _audioManager;
    private bool _slowedDown;

    [Header("Testing Variables")]
    public bool speedIncreaseBool;

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
        //  _timeManager.OnTimeChanged += (s, e) => { if (_slowedDown) { _slowedDown = false; OriginalSpeed(); } else { _slowedDown = true; SlowDownSpeed(); } };
    }
    private void AssignSource(GameObject ball)
    {
        _audioManager.IsBall = true;

        var script = ball.AddComponent<ColorChangeScript>();
        script.AudioManager = _audioManager;
        script.Colors = _colors;
        var script2 = ball.GetComponent<Ball>();
        script2.BallBounced += (s, e) => { SpeedIncrease(); };
    }

    private void FixedUpdate()
    {
        _audioManager.FixedUpdate();
        if (speedIncreaseBool)
        {
            SpeedIncrease(); speedIncreaseBool = false;
        }
    }

    private void SpeedIncrease()
    {
        _audioManager.SpeedIncrease(_pitchIncrease);
    }
    private void OriginalSpeed()
    {
        _pitchIncrease /= _timeSlowAmount;
        _audioManager.OriginalSpeed(_timeSlowAmount);
    }

    private void SlowDownSpeed()
    {
        _pitchIncrease *= _timeSlowAmount;
        _audioManager.SlowDown(_timeSlowAmount);
    }
}
