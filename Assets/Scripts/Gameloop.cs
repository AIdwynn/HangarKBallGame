using System;
using System.Collections.Generic;
using UnityEngine;

public class Gameloop : MonoBehaviour
{

    [Header("Gamedesign Variables")]
    [SerializeField] private float _pitchIncrease;
    [SerializeField] private float _maxTimeSlowAmount;

    [Header("Music Choices")]
    [SerializeField] private int _amountOfBeeps;
    [SerializeField] private AudioClip _beepSound;
    [SerializeField] private AudioClip _boopSound;
    [SerializeField] private SongNames _startSongName;

    [Header("Scene Dependant Varaiables")]
    [SerializeField] private TileMapManager _tileMapManager;
    [SerializeField] private List<HeadBob> _heads;

    [Header("Unchangeable Values")]
    [SerializeField] private MusicData _musicData;
    [SerializeField] private AudioSource _beepSource;
    [SerializeField] private AudioSource _musicSource;
    [SerializeField] private AudioSource _characterSource;
    [SerializeField] private AudioSource _sfxSource;

    private AudioManager _audioManager;


    [Header("Testing Variables")]
    public bool speedIncreaseBool;

    private void Awake()
    {
        _audioManager = new AudioManager(_beepSound, _boopSound, _beepSource, _startSongName, _musicData, _musicSource, this, _maxTimeSlowAmount, _amountOfBeeps, _characterSource, _sfxSource);
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

    public void PlaySound(SoundNames name, bool IsPlayer) => _audioManager.PlaySound(name, IsPlayer);
    internal void BPMChange(float v, float w)
    {
        _tileMapManager.ChangeSpeed(v/w);
        foreach (var item in _heads)
        {
            item.BPM = v;
        }
    }
}
