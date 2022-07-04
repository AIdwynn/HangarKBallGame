using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DAE.Gamesystem.Singleton;
using System;

public class BoopEventargs : EventArgs
{

}

public class AudioManager
{
    private int _boopCounter;
    private int _boopMoment = 3;
    private int _bPM;
    private AudioClip _beepSound;
    private AudioClip _boopSound;
    private AudioSource _musicSource;
    public EventHandler<BoopEventargs> boopHandler;
    public bool IsBall;
    private float _counter;

    public AudioManager(int bPM, AudioClip beepSound, AudioClip boopSound, AudioSource musicSource)
    {
        _bPM = bPM;
        _beepSound = beepSound;
        _boopSound = boopSound;
        _musicSource = musicSource;
    }

    public void FixedUpdate()
    {
        var timeBetweenSounds = 60 / ((float)_bPM * (_boopMoment+1));
        _counter += Time.fixedDeltaTime;
        if (timeBetweenSounds < _counter) { PlayNextSound(); _counter = 0; }
    }

    private void PlayNextSound()
    {
        if (_boopCounter == _boopMoment)
        {
            _musicSource.clip = _boopSound;
            _musicSource.Play();
            if(IsBall)
                ThrowBoopEvent();
            _boopCounter = 0;
        }
        else
        {
            _musicSource.clip = _beepSound;
            _musicSource.Play();
            _boopCounter++;
        }

    }

    private void ThrowBoopEvent()
    {
        var handler = boopHandler;
        handler.Invoke(this, new BoopEventargs());
    }
}
