using DAE.Gamesystem.Singleton;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleSpawner : SingletonMonoBehaviour<ParticleSpawner>
{
    [Header("animation")]
    [SerializeField] private ParticleSystem _directionalParticles, _constantParticles, _spreadingPArticles;


    private ParticleSystem.MinMaxGradient _currentGradient;



    //setColour of Particle
    public void SetParticleColourBasedOnCollision(ParticleToPlay particleType, Transform objectToChangeColourToo)
    {
        if (objectToChangeColourToo != null)
        {
            if (objectToChangeColourToo.transform.TryGetComponent(out SpriteRenderer r))
            {
                _currentGradient = new ParticleSystem.MinMaxGradient(r.color * 0.9f, r.color * 1.2f);

                switch (particleType)
                {
                    case ParticleToPlay.Directional:
                        SetColor(_directionalParticles);
                        break;
                    case ParticleToPlay.Constant:
                        SetColor(_constantParticles);
                        break;
                    case ParticleToPlay.Spreading:
                        SetColor(_spreadingPArticles);
                        break;
                    default:
                        break;
                }                
            }
        }
        
    }

    //play burst playparticles
    public void PlayDirectionalParticles(Transform PlayLocation, float ZRotationOfCollosion)
    {
        _directionalParticles.transform.rotation = Quaternion.Euler(0, 0, ZRotationOfCollosion);
        _directionalParticles.transform.position = PlayLocation.position;    
        _directionalParticles.Play();
    }

    public void SetDirectionalParticleSpeed(float minSpeed, float maxSpeed)
    {
        var main = _directionalParticles.main;

        var mult = Random.Range(minSpeed, maxSpeed);

        main.startSpeedMultiplier = mult;
        main.startLifetimeMultiplier = mult;
    }

    public void SetDirectionalParticleSize(float minSize, float maxSize)
    {
        var main = _directionalParticles.main;

        var mult = Random.Range(minSize, maxSize);

        main.startSizeMultiplier = mult;
    }


    //must be played in update
    public void PlayConstantParticle(Transform PlayLocation)
    {
        _constantParticles.transform.position = PlayLocation.position;
        _constantParticles.Play();
    }

    public void SetconstantParticleSpeed(float minSpeed, float maxSpeed)
    {
        var main = _constantParticles.main;

        var mult = Random.Range(minSpeed, maxSpeed);

        main.startSpeedMultiplier = mult;
        main.startLifetimeMultiplier = mult;
    }

    public void SetconstantParticleSize(float minSize, float maxSize)
    {
        var main = _constantParticles.main;

        var mult = Random.Range(minSize, maxSize);

        main.startSizeMultiplier = mult;
        
    }


    public void MoveConstantParticle(Transform PlayLocation)
    {
        _constantParticles.transform.position = PlayLocation.position;
    }

    public void StopConstantParticle()
    {
        _constantParticles.Stop();
    }


    public void PlaySpreadingParticles(Transform PlayLocation)
    {
        _spreadingPArticles.transform.position = PlayLocation.position;
        _spreadingPArticles.Play();
    }

    public void SetSpreadingParticleSpeed(float minSpeed, float maxSpeed)
    {
        var main = _spreadingPArticles.main;

        var mult = Random.Range(minSpeed, maxSpeed);

        main.startSpeedMultiplier = mult;
        main.startLifetimeMultiplier = mult;
    }

    public void SetSpreadingParticleSize(float minSize, float maxSize)
    {
        var main = _spreadingPArticles.main;

        var mult = Random.Range(minSize, maxSize);

        main.startSizeMultiplier = mult;

    }





    void SetColor(ParticleSystem ps)
    {
        var main = ps.main;
        main.startColor = _currentGradient;
    }
}

public enum ParticleToPlay
{
    Directional,
    Constant,
    Spreading
}
