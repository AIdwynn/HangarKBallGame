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

            new ParticleSpawner(ParticleDirection.Directional, ParticleShape.Trigangles)
                          .ChangeSpeed(2f, 2.6f)
                          .ChangeSize(0.7f, 1.5f)
                          .SetColour(Color.blue, Color.cyan)
                          .Rotate(Random.Range(0, 360))
                          .Activate(transform);

            new ParticleSpawner(ParticleDirection.Directional, ParticleShape.Squares)
                               .ChangeSpeed(1f, 1.5f)
                               .ChangeSize(0.8f, 1f)
                               .SetColour(Color.red, Color.yellow)
                               .Rotate(Random.Range(0, 360))
                               .Activate(secondlocation);



            timer = 0;
        }

    }


}



