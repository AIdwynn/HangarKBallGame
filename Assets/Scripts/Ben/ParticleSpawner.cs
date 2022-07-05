using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleSpawner
{

    private ParticleSystem _currentSystem;
    private ParticleType _particleType;

    public ParticleSpawner(ParticleType particleType)
    {
        _currentSystem = ParticleObjectPool.Instance.GetParticleSystem(particleType);
        _particleType = particleType;
    }

    public ParticleSpawner Activate(Transform PlayLocation)
    {
        if (_particleType == ParticleType.Constant)
        {
            ParticleObjectPool.Instance.PlayConstantParticleSystem(_currentSystem, PlayLocation);
            return this;
        }
        ParticleObjectPool.Instance.PlayOneShotParticleSystem(_currentSystem, PlayLocation);

        return this;
    }

    public ParticleSpawner StopConstantAfterSeconds(float time)
    {
        
        ParticleObjectPool.Instance.StopConstantParticleSystemAfterSeconds(_currentSystem, time);

        return this;
    }   

    public ParticleSpawner StopConstant()
    {
        ParticleObjectPool.Instance.StopConstantParticleSystem(_currentSystem);

        return this;
    }

    public ParticleSpawner SetParticleColourBasedOnObject(Transform objectToChangeColourToo)
    {
        ParticleObjectPool.Instance.SetParticleColourBasedOnCollision(_currentSystem, objectToChangeColourToo);

        return this;
    }

    public ParticleSpawner SetColour(Color color)
    {
        ParticleObjectPool.Instance.SetColor(_currentSystem, color);

        return this;
    }

    public ParticleSpawner Rotate(float Zrotation)
    {
        ParticleObjectPool.Instance.RotateParticleSystemZ(_currentSystem, Zrotation);

        return this;
    }

    public ParticleSpawner ChangeSpeed(float minSpeed, float maxSpeed)
    {
        ParticleObjectPool.Instance.SetDirectionalParticleSpeed(_currentSystem, minSpeed, maxSpeed);

        return this;
    }

    public ParticleSpawner ChangeSize(float minSize, float maxSzie)
    {
        ParticleObjectPool.Instance.SetDirectionalParticleSize(_currentSystem, minSize, maxSzie);

        return this;
    }



}
