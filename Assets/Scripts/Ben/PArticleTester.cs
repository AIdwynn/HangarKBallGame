using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class ParticleTester : MonoBehaviour
{
    public Transform secondlocation; 

    float timer = 0f;


    private void Update()
    {
        timer += Time.deltaTime;

        if (timer >= 2)
        {

            new ParticleSpawner(ParticleType.Directional)
                          .ChangeSpeed(2f, 2.6f)
                          .ChangeSize(0.1f, 0.6f)
                          .SetColour(Color.black)
                          .Rotate(Random.Range(0, 360))
                          .Activate(transform);

            new ParticleSpawner(ParticleType.Directional)
                               .ChangeSpeed(1f, 1.5f)
                               .ChangeSize(0.5f, 1f)
                               .Rotate(Random.Range(0, 360))
                               .Activate(secondlocation);


            timer = 0;
        }

    }


}



