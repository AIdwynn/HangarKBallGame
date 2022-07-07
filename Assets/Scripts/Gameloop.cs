using System.Collections.Generic;
using UnityEngine;

public class Gameloop : MonoBehaviour
{

    [Header("Gamedesign Variables")]
    [SerializeField] private int mongolen;
    [SerializeField] private float _pitchIncrease;
    [SerializeField] private float _maxTimeSlowAmount;
    [SerializeField] private AudioClip _beepSound;
    [SerializeField] private AudioClip _boopSound;
    [SerializeField] private SongNames _startSongName;



    [Header("Unchangeable Values")]
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private MusicData _musicData;
    [SerializeField] private AudioSource _musicSource;
    [SerializeField] private TileMapManager _tileMapManager;
    private AudioManager _audioManager;


    [Header("Testing Variables")]
    public bool speedIncreaseBool;

    private void Awake()
    {
        _audioManager = new AudioManager(_beepSound, _boopSound, _audioSource, _startSongName, _musicData, _musicSource, _tileMapManager, _maxTimeSlowAmount, mongolen);
    }

    private void FixedUpdate()
    {
        _audioManager.FixedUpdate();
    }

    public void TimeSpeedChange(float normalisedTimeScale)
    {
        _audioManager.ChangeBpm(normalisedTimeScale);
    }

    public void ChangeSong(SongNames name)
    {
        _audioManager.ChangeSong(name);
    }

}
