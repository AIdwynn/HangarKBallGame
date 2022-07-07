using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleSpawner
{

    private ParticleSystem _currentSystem;
    private ParticleDirection _particleType;

    public ParticleSpawner(ParticleDirection particleType, ParticleShape particleShape)
    {
        _currentSystem = ParticleManager.Instance.GetParticleSystem(particleType);
        ParticleManager.Instance.SetMaterial(_currentSystem, particleShape);
        _particleType = particleType;
    }

    public ParticleSpawner Activate(Transform PlayLocation)
    {
        if (_particleType == ParticleDirection.Constant)
        {
            ParticleManager.Instance.PlayConstantParticleSystem(_currentSystem, PlayLocation);
            return this;
        }
        ParticleManager.Instance.PlayOneShotParticleSystem(_currentSystem, PlayLocation);

        return this;
    }

    public ParticleSpawner StopConstantAfterSeconds(float time)
    {
        
        ParticleManager.Instance.StopConstantParticleSystemAfterSeconds(_currentSystem, time);

        return this;
    }   

    public ParticleSpawner StopConstant()
    {
        ParticleManager.Instance.StopConstantParticleSystem(_currentSystem);

        return this;
    }

    public ParticleSpawner SetParticleColourBasedOnObject(Transform objectToChangeColourToo)
    {
        ParticleManager.Instance.SetParticleColourBasedOnCollision(_currentSystem, objectToChangeColourToo);

        return this;
    }

    public ParticleSpawner SetColour(Color color1)
    {
        ParticleManager.Instance.SetColorSingle(_currentSystem, color1);

        return this;
    }

    public ParticleSpawner SetColourGradient(Color color1, Color color2)
    {
        ParticleManager.Instance.SetColorGradient(_currentSystem, color1, color2);

        return this;
    }

    public ParticleSpawner Rotate(float Zrotation)
    {
        ParticleManager.Instance.RotateParticleSystemZ(_currentSystem, Zrotation);

        return this;
    }

    public ParticleSpawner ChangeSpeed(float minSpeed, float maxSpeed)
    {
        ParticleManager.Instance.SetParticleSpeed(_currentSystem, minSpeed, maxSpeed);

        return this;
    }

    public ParticleSpawner ChangeLifeTime(float MinLifeTime, float Maxlifetime)
    {
        ParticleManager.Instance.SetParticleSpeed(_currentSystem, MinLifeTime, Maxlifetime);

        return this;
    }

    public ParticleSpawner ChangeSize(float minSize, float maxSzie)
    {
        ParticleManager.Instance.SetParticleSize(_currentSystem, minSize, maxSzie);

        return this;
    }



}
