using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DAE.Gamesystem.Singleton;
using System;

public class AudioManager
{
    private int _boopCounter;
    private int _boopMoment = 3;
    private float _bPM;
    private AudioClip _song;
    private AudioClip _beepSound;
    private AudioClip _boopSound;
    private AudioSource _boopSource;
    private AudioSource _musicSource;
    public EventHandler boopHandler;
    public bool IsBall = false;
    private float _timeModifier =1; 
    private float _counter;

    public AudioManager(AudioClip beepSound, AudioClip boopSound, AudioSource boopSource, SongNames songName, MusicData musicData, AudioSource musicSource)
    {
        _beepSound = beepSound;
        _boopSound = boopSound;
        _boopSource = boopSource;
        _musicSource = musicSource;

        foreach (var item in musicData.songs)
        {
            if (item.names == songName)
            {
                _bPM = item.bpm;
                _song = item.song;
                Debug.Log(_bPM);
            }
        }
        _musicSource.clip = _song;
        _musicSource.loop = true;
        _musicSource.Play();

    }

    public void FixedUpdate()
    {
        var timeBetweenSounds = 60 / ((float)_bPM*_timeModifier);
        _counter += Time.fixedDeltaTime;
        if (timeBetweenSounds < _counter) { PlayNextSound(); _counter = 0; }
    }

    private void PlayNextSound()
    {
        if (_boopCounter == _boopMoment)
        {
            _boopSource.clip = _boopSound;
            _boopSource.Play();
            if(IsBall)
                ThrowBoopEvent();
            _boopCounter = 0;
        }
        else
        {
            _boopSource.clip = _beepSound;
            _boopSource.Play();
            _boopCounter++;
        }

    }

    private void ThrowBoopEvent()
    {
        //var handler = boopHandler;
        //handler.Invoke(this, new EventArgs());
        //TileMapManager.ChangeColor(_bPM * _timeModifier);
    }

    public void SpeedIncrease(float pitchIncrease)
    {
        //_musicSource.pitch += pitchIncrease;
        //_boopSource.pitch += pitchIncrease;
        //_timeModifier += pitchIncrease;

        var ogLength = _musicSource.clip.length/_musicSource.pitch;
        _musicSource.pitch += pitchIncrease;
        _boopSource.pitch += pitchIncrease;
        _bPM = _bPM * (1 / ((_musicSource.clip.length / _musicSource.pitch)/ ogLength));


    }

    internal void OriginalSpeed(float timeSlowAmount)
    {
        var ogLength = _musicSource.clip.length / _musicSource.pitch;
        _musicSource.pitch /= timeSlowAmount;
        _boopSource.pitch /= timeSlowAmount;
        _bPM = _bPM * (1 / ((_musicSource.clip.length / _musicSource.pitch) / ogLength));

    }

    internal void SlowDown(float timeSlowAmount)
    {
        var ogLength = _musicSource.clip.length / _musicSource.pitch;
        _musicSource.pitch *= timeSlowAmount;
        _boopSource.pitch *= timeSlowAmount;
        _bPM = _bPM * (1 / ((_musicSource.clip.length / _musicSource.pitch) / ogLength));
    }
}
