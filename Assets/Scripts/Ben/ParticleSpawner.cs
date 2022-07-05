using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleSpawner
{

    private ParticleSystem _currentSystem;
    private ParticleDirection _particleType;

    public ParticleSpawner(ParticleDirection particleType, ParticleShape particleShape)
    {
        _currentSystem = ParticleObjectPool.Instance.GetParticleSystem(particleType);
        ParticleObjectPool.Instance.SetMaterial(_currentSystem, particleShape);
        _particleType = particleType;
    }

    public ParticleSpawner Activate(Transform PlayLocation)
    {
        if (_particleType == ParticleDirection.Constant)
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

    public ParticleSpawner SetColour(Color color1, Color color2)
    {
        ParticleObjectPool.Instance.SetColor(_currentSystem, color1, color2);

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
