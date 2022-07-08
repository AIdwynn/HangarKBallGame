using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DAE.Gamesystem.Singleton;
using System;

public class AudioManager
{
    private int _boopCounter;
    private int _boopMoment;
    private float _startBPM;
    private float _bPM;
    private MusicData _data;
    private AudioClip _song;
    private AudioClip _beepSound;
    private AudioClip _boopSound;
    private AudioSource _boopSource;
    private AudioSource _musicSource;
    private AudioSource _characterSource;
    private AudioSource _sfxSource;
    private Gameloop _gameloop;
    private float _slowAmount;
    private float _timeModifier =1; 
    private float _counter;

    public AudioManager(AudioClip beepSound, AudioClip boopSound, AudioSource boopSource, SongNames songName, MusicData musicData, AudioSource musicSource, Gameloop gameloop, float slowAmount, int boopMoment, AudioSource characterSource, AudioSource sfxSource)
    {
        _beepSound = beepSound;
        _boopSound = boopSound;
        _boopSource = boopSource;
        _musicSource = musicSource;
        _musicSource.loop = true;
        _gameloop = gameloop;
        _slowAmount = slowAmount;
        _data = musicData;
        _boopMoment = boopMoment;
        _characterSource = characterSource;
        _sfxSource = sfxSource;

        ChangeSong(songName);

    }

    public void ChangeSong(SongNames songName)
    {
        foreach (var item in _data.Songs)
        {
            if (item.Name == songName)
            {
                _startBPM = item.BPM;
                _bPM = item.BPM;
                _song = item.SongClip;
                Debug.Log(_bPM);
            }
        }
        BPMChange();
        _musicSource.clip = _song;
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
        if (_boopCounter == _boopMoment-1)
        {
            _boopSource.clip = _boopSound;
            _boopSource.Play();
            _boopCounter = 0;
        }
        else
        {
            _boopSource.clip = _beepSound;
            _boopSource.Play();
            _boopCounter++;
        }

    }

    internal void ChangeBpm(float normalisedTimeScale)
    {
        _musicSource.pitch = Mathf.Lerp(_musicSource.pitch, _slowAmount, normalisedTimeScale);
        _boopSource.pitch = Mathf.Lerp(_musicSource.pitch, _slowAmount, normalisedTimeScale);
        _bPM = _startBPM * (1 / ((_musicSource.clip.length / _musicSource.pitch) / _musicSource.clip.length));
        BPMChange();

    }

    private void BPMChange()
    {
        _gameloop.BPMChange(_bPM, _boopMoment);
    }

    public void PlaySound(SoundNames name, bool IsPlayer)
    {
        foreach (var item in _data.Sounds)
        {
            if (item.Name == name)
            {
                if (IsPlayer)
                {
                    _characterSource.clip = item.SoundClip;
                    _characterSource.Play();
                }
                else
                {
                    _sfxSource.clip = item.SoundClip;
                    _sfxSource.Play();
                }

            }
        }
    }
}

