using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class ParticleTester : MonoBehaviour
{
    public Transform TestHandleLocation;

    float timer = 0f;


    private void Update()
    {
        timer += Time.deltaTime;

        if (timer >= 2)
        {
            // to use the particle system make sure the ParticleManager Prefab is in the Scene

            //Non cosstant ones are used once, they jus tplay in the world 1x at given location
            new ParticleSpawner(ParticleDirection.Directional, ParticleShape.triangles)    // initialize particle system with Type and material
                          .ChangeSpeed(2f, 2.6f)                                        // Change speed of Particle -> uses multiplication of the current speed with 1 keeping it the same 
                          .ChangeSize(0.7f, 1.5f)                                       // Change size of Particle -> uses multiplication of the current speed with 1 keeping it the same 
                          .SetColourGradient(Color.blue, Color.cyan)                    // Set Colour gradient of particle
                          .Rotate(Random.Range(0, 360))                                 // Rotate Particle System on Z axis, only relevant for directional Ones
                          .Activate(transform);                                         // Activate PArticleSystem at location ( not parented)

            new ParticleSpawner(ParticleDirection.Spreading, ParticleShape.Squares)     // initialize particle system with Type and material
                               .ChangeSpeed(1f, 1.5f)                                   // Change speed of Particle -> uses multiplication of the current speed with 1 keeping it the same 
                               .ChangeLifeTime(0.2f, 1.3f)                              // Change LifeTime of Particle -> uses multiplication of the current speed with 1 keeping it the same    
                               .ChangeSize(0.8f, 1f)                                    // Change size of Particle -> uses multiplication of the current speed with 1 keeping it the same 
                               .SetColour(Color.red)                                    // Set Colour particle
                               .Activate(TestHandleLocation);                               // Activate PArticleSystem at location ( not parented)

            //constant particly system will chilld itself to the object and play constantly untill turned off manually or after x seconds.
            ParticleSpawner ConstantSystem = new ParticleSpawner(ParticleDirection.Constant, ParticleShape.Squares)   // initialize particle system with Type and material
                                .ChangeSize(0.1f, 0.3f)                                 // Change size of Particle -> uses multiplication of the current speed with 1 keeping it the same 
                                .SetColourGradient(Color.white, Color.blue)             // Set Colour gradient of particle
                                .Activate(TestHandleLocation)              // Activate -> in case of constant particles, will child itself to passed in object. 
                                .StopConstantAfterSeconds(1f);                          // Will Destroy and stop the constant particle system after x Seconds


            //ConstantSystem.StopConstant();
            // StopConstant can also be used Later in the script with the New ParticleSpawner Reference

            //if buggs LMK.


            timer = 0;
        }

    }


}



