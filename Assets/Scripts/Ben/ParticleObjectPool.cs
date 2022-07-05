using DAE.Gamesystem.Singleton;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleObjectPool : SingletonMonoBehaviour<ParticleObjectPool>
{
    //[Header("animation")]
    //[SerializeField] private ParticleSystem _directionalParticles, _constantParticles, _spreadingPArticles;

    [SerializeField] private GameObject _directionalParticleObject, _constantParticleObject, _spreadingPArticleObject;

    //[SerializeField] private List<GameObject> _directionalParticles, _constantParticles, _spreadingParticles;

    //private const int INITIAL_POOL_SIZE = 5;
    //private const int MAX_POOL_SIZE = 20;

    private ParticleSystem.MinMaxGradient _currentGradient;  

    //private void Awake()
    //{
    //    //for (int i = 0; i < INITIAL_POOL_SIZE; i++)
    //    //{
    //    //    GenerateDirectionalParticleSystem();
    //    //    GenerateSpreadingParticleSystem();
    //    //    GenerateConstantParticleSystem();
    //    //}
    //}

    //fill up object pools.
    //private void GenerateDirectionalParticleSystem()
    //{
    //    GameObject newParticleSystem = Instantiate(_directionalParticleObject, transform);
    //    newParticleSystem.SetActive(false);
    //    _directionalParticles.Add(newParticleSystem);
    //}

    //private void GenerateSpreadingParticleSystem()
    //{
    //    GameObject newParticleSystem = Instantiate(_spreadingPArticleObject, transform);
    //    newParticleSystem.SetActive(false);
    //    _spreadingParticles.Add(newParticleSystem);
    //}

    //private void GenerateConstantParticleSystem()
    //{
    //    GameObject newParticleSystem = Instantiate(_constantParticleObject, transform);
    //    newParticleSystem.SetActive(false);
    //    _constantParticles.Add(newParticleSystem);
    //}

    public ParticleSystem GetParticleSystem(ParticleType particleType)
    {
        switch (particleType)
        {
            case ParticleType.Directional:
                return GetParticle(_directionalParticleObject);
             
            case ParticleType.Constant:
                return GetParticle(_constantParticleObject);
              
            case ParticleType.Spreading:
                return GetParticle(_spreadingPArticleObject);
                
            default:
                return null;
              
        }
        //Try to find an inactive bullet
        
    }

    private ParticleSystem GetParticle(GameObject particleobject)
    {

        GameObject newParticleSystem = Instantiate(particleobject, transform);
        return newParticleSystem.GetComponent<ParticleSystem>();

        //for (int i = 0; i < ParticleList.Count; i++)
        //{
        //    GameObject ThisParticleSystem = ParticleList[i];

        //    if (!ThisParticleSystem.activeSelf)
        //    {
        //        return ThisParticleSystem.GetComponent<ParticleSystem>();
        //    }
        //}

        ////We are out of bullets so we have to instantiate another bullet (if we can)
        //if (ParticleList.Count < MAX_POOL_SIZE)
        //{
        //    GenerateDirectionalParticleSystem();

        //    //The new bullet is last in the list so get it
        //    GameObject LastPArticleSystem = ParticleList[ParticleList.Count - 1];

        //    return LastPArticleSystem.GetComponent<ParticleSystem>();
        //}

        //return null;
    }

    public void PlayOneShotParticleSystem(ParticleSystem ParticleSystem, Transform PlayLocation)
    {
        ParticleSystem.gameObject.SetActive(true);

        ParticleSystem.transform.position = PlayLocation.position;

        var main = ParticleSystem.main;
        main.stopAction = ParticleSystemStopAction.Destroy;

        ParticleSystem.Play();
    }


    //Call on update
    public void PlayConstantParticleSystem(ParticleSystem ParticleSystem, Transform PlayLocation)
    {
        ParticleSystem.transform.parent = PlayLocation;
        //var main = ParticleSystem.main;
        //main.stopAction = ParticleSystemStopAction.Destroy;
        ParticleSystem.Play();
    }
    public void StopConstantParticleSystem(ParticleSystem ParticleSystem)
    {
        ParticleSystem.Stop();
        Destroy(ParticleSystem.gameObject);

    }
    public void StopConstantParticleSystemAfterSeconds(ParticleSystem ParticleSystem, float time)
    {
        StartCoroutine(StopConstantPS(ParticleSystem, time));
    }
    IEnumerator StopConstantPS(ParticleSystem ParticleSystem, float time)
    {
        yield return new WaitForSeconds(time);

        ParticleSystem.Stop();
        Destroy(ParticleSystem.gameObject);
    }


    //extensionmethods

    //setColour of Particle
    public void SetParticleColourBasedOnCollision(ParticleSystem PArticleSystem, Transform objectToChangeColourToo)
    {
        if (objectToChangeColourToo != null)
        {
            if (objectToChangeColourToo.transform.TryGetComponent(out SpriteRenderer r))
            {
                _currentGradient = new ParticleSystem.MinMaxGradient(r.color * 0.9f, r.color * 1.2f);

                SetColor(PArticleSystem);               
            }            
        }        
    }
    void SetColor(ParticleSystem ps)
    {
        var main = ps.main;
        main.startColor = _currentGradient;
    }

    public void SetColor(ParticleSystem ps, Color color)
    {
        var main = ps.main;
        main.startColor = _currentGradient = new ParticleSystem.MinMaxGradient(color * 0.9f, color * 1.2f);
    }

    //RotateParticleSystem
    public void RotateParticleSystemZ(ParticleSystem ParticleSystem, float ZRotationOfCollosion)
    {
        ParticleSystem.transform.rotation = Quaternion.Euler(0, 0, ZRotationOfCollosion);

        ParticleSystem.Play();

        var main = ParticleSystem.main;

        main.stopAction = ParticleSystemStopAction.Destroy;
    }

    public void SetDirectionalParticleSpeed(ParticleSystem ParticleSystem, float minSpeed, float maxSpeed)
    {
        var main = ParticleSystem.main;

        var mult = Random.Range(minSpeed, maxSpeed);

        main.startSpeedMultiplier = mult;
        main.startLifetimeMultiplier = mult;
    }

    public void SetDirectionalParticleSize(ParticleSystem ParticleSystem, float minSize, float maxSize)
    {
        var main = ParticleSystem.main;

        var mult = Random.Range(minSize, maxSize);

        main.startSizeMultiplier = mult;
    }
       

}

public enum ParticleType
{
    Directional,
    Constant,
    Spreading
}
