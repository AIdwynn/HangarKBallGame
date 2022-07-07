using DAE.Gamesystem.Singleton;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ParticleManager : SingletonMonoBehaviour<ParticleManager>
{
    //[Header("animation")]
    //[SerializeField] private ParticleSystem _directionalParticles, _constantParticles, _spreadingPArticles;

    //Add materialshere, (change template materials)
    [Header("Materials")]
    [SerializeField] private Material SmokeClouds;
    [SerializeField] private Material cubes, triangles, template1, template2, template3;

    [SerializeField] private Sprite[] SmokeCloudsSprites;
    [SerializeField] private Sprite[] cubesSprites, trianglesSprites, template1Sprites, template2Sprites, template3Sprites;

    [Header("ParticleSystems")]
    [SerializeField] private GameObject _directionalParticleObject;
    [SerializeField] private GameObject _constantParticleObject, _spreadingPArticleObject;

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

    public ParticleSystem GetParticleSystem(ParticleDirection particleType)
    {
        switch (particleType)
        {
            case ParticleDirection.Directional:
                return GetParticle(_directionalParticleObject);

            case ParticleDirection.Constant:
                return GetParticle(_constantParticleObject);

            case ParticleDirection.Spreading:
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
            if (objectToChangeColourToo.transform.TryGetComponent(out Tilemap r))
            {
                _currentGradient = new ParticleSystem.MinMaxGradient(r.color * 0.5f, r.color * 1.5f);

                SetColor(PArticleSystem);
            }
        }
    }
    void SetColor(ParticleSystem ps)
    {
        var main = ps.main;

        main.startColor = _currentGradient;
    }

    public void SetColorSingle(ParticleSystem ps, Color color)
    {
        var main = ps.main;
        main.startColor = color;
    }

    public void SetColorGradient(ParticleSystem ps, Color color1, Color color2)
    {
        var main = ps.main;
        main.startColor = _currentGradient = new ParticleSystem.MinMaxGradient(color1, color2);
    }

    //RotateParticleSystem
    public void RotateParticleSystemZ(ParticleSystem ParticleSystem, float ZRotationOfCollosion)
    {
        ParticleSystem.transform.rotation = Quaternion.Euler(0, 0, ZRotationOfCollosion);

        ParticleSystem.Play();


    }

    public void SetParticleSpeed(ParticleSystem ParticleSystem, float minSpeed, float maxSpeed)
    {
        var main = ParticleSystem.main;

        var mult = Random.Range(minSpeed, maxSpeed);

        main.startSpeedMultiplier = mult;

    }

    public void SetParticleLifeTime(ParticleSystem ParticleSystem, float minSpeed, float maxSpeed)
    {
        var main = ParticleSystem.main;

        var mult = Random.Range(minSpeed, maxSpeed);

        main.startLifetimeMultiplier = mult;
    }

    private void ChangeMaterial(Material mat, ParticleSystem particleSystem, Sprite[] sprites)
    {
        particleSystem.gameObject.GetComponent<ParticleSystemRenderer>().material = mat;
        for (int i = 0; i < sprites.Length - 1; i++)
        {
            particleSystem.gameObject.GetComponent<ParticleSystem>().textureSheetAnimation.SetSprite(i, sprites[i]);
        }
    }

    public void SetParticleSize(ParticleSystem ParticleSystem, float minSize, float maxSize)
    {
        var main = ParticleSystem.main;

        var mult = Random.Range(minSize, maxSize);

        main.startSizeMultiplier = mult;
    }
    public void SetMaterial(ParticleSystem particleSystem, ParticleShape shape)
    {
        switch (shape)
        {
            case ParticleShape.Clouds:
                ChangeMaterial(SmokeClouds, particleSystem, SmokeCloudsSprites);
                break;
            case ParticleShape.Squares:
                ChangeMaterial(cubes, particleSystem, cubesSprites);
                break;
            case ParticleShape.cirkles:
                ChangeMaterial(triangles, particleSystem, trianglesSprites);
                break;
            case ParticleShape.template1:
                ChangeMaterial(template1, particleSystem, template1Sprites);
                break;
            case ParticleShape.template2:
                ChangeMaterial(template2, particleSystem, template2Sprites);
                break;
            case ParticleShape.template3:
                ChangeMaterial(template3, particleSystem, template3Sprites);
                break;
            default:
                break;
        }
    }    

}


//if u wanna add different types of systems, let me know, thats a bit more difficult
public enum ParticleDirection
{
    Directional,
    Constant,
    Spreading
}

//add material here if needed, change templates with F2, plug them into the right sockets in inspector
public enum ParticleShape
{
    Clouds,
    Squares,
    cirkles,
    template1,
    template2,
    template3
}
