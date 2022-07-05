using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]

public class PArticleTester : MonoBehaviour
{
    public Transform secondlocation;

    public List<Transform> randomColours;

    public bool PlayConstant = false;

    public bool stopConstant = false;


    public bool PlayDirectional = false;

    public bool PlaySpreading = false;

    private void Start()
    {
      
    }

    private void Update()
    {
        ConstantParticle();

        if (PlayDirectional)
        {
            ParticleSpawner.Instance.SetParticleColourBasedOnCollision(ParticleToPlay.Directional, randomColours[Random.Range(0, randomColours.Count)]);
            ParticleSpawner.Instance.SetDirectionalParticleSize(0.5f, 1.5f);
            ParticleSpawner.Instance.SetDirectionalParticleSpeed(2f, 5f);
            ParticleSpawner.Instance.PlayDirectionalParticles(transform, Random.Range(0, 360));
            PlayDirectional = false;
        }

        if (PlaySpreading)
        {
            ParticleSpawner.Instance.SetParticleColourBasedOnCollision(ParticleToPlay.Spreading, randomColours[Random.Range(0, randomColours.Count)]);
            ParticleSpawner.Instance.SetSpreadingParticleSpeed(0.5f, 1.5f);
            ParticleSpawner.Instance.SetSpreadingParticleSize(0.5f, 1.5f);
            ParticleSpawner.Instance.PlaySpreadingParticles(transform);
            PlaySpreading = false;
        }
    }

    private void ConstantParticle()
    {
        if (PlayConstant)
        {
            ParticleSpawner.Instance.PlayConstantParticle(transform);
            
            PlayConstant = false;
        }
        if (stopConstant)
        {
            ParticleSpawner.Instance.StopConstantParticle();
            
            stopConstant = false;
        }
        //ParticleSpawner.Instance.SetParticleColourBasedOnCollision(ParticleToPlay.Constant, randomColours[Random.Range(0, randomColours.Count - 1)]);
        ParticleSpawner.Instance.MoveConstantParticle(transform);
    }
}
