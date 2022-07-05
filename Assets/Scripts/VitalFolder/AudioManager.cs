using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DAE.Gamesystem.Singleton;
using System;

public class AudioManager
{
    private int _boopCounter;
    private int _boopMoment = 3;
    private int _bPM;
    private AudioClip _song;
    private AudioClip _beepSound;
    private AudioClip _boopSound;
    private AudioSource _boopSource;
    private AudioSource _musicSource;
    public EventHandler boopHandler;
    public bool IsBall = true;
    private float _counter;

    public AudioManager(AudioClip beepSound, AudioClip boopSound, AudioSource boopSource, SongNames _songName, MusicData musicData, AudioSource musicSource)
    {
        _beepSound = beepSound;
        _boopSound = boopSound;
        _boopSource = boopSource;
        _musicSource = musicSource;

        foreach (var item in musicData.songs)
        {
            if (item.names == _songName)
            {
                _bPM = item.bpm;
                _song = item.song;
            }
        }
        _musicSource.loop = true;
        _musicSource.Play();

    }

    public void FixedUpdate()
    {
        var timeBetweenSounds = 60 / ((float)_bPM);
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
        var handler = boopHandler;
        handler.Invoke(this, new EventArgs());
    }
}
