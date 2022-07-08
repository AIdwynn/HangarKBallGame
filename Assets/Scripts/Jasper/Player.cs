using DG.Tweening;
using System.Collections;
using UnityEngine;
using static UnityEngine.InputSystem.InputAction;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float _speed;

    [SerializeField]
    private Rigidbody2D _rb;

    private PlayerInputControls _playerInput;

    private Vector2 _inputVal;

    private Vector2 _aimInputVector;

    [SerializeField]
    private float _reflectorDistance;

    [SerializeField]
    private GameObject _reflector;

    [SerializeField]
    private float _maxVel;

    public bool _TESTSLOW;


    [SerializeField]
    private float _playerPushbackForce;


    private Transform _fromRotReflect;

    [SerializeField]
    private float _smoothnessAim;

    [SerializeField]
    private float _animScale;

    [SerializeField]
    private float _animDuration;

    [SerializeField]
    private float _shakeDuration;

    ParticleSpawner _ps;

    private void Start()
    {



        _rb.gravityScale = 0;


        _aimInputVector = new Vector2(-1, 0);

        StartCoroutine(PlayerAnim());

        //   _playerInput.PlayerActionMap.Aim.canceled += AimCanceled;


        // Rumbler.Instance.RumblePulse(0.5f, 1f, 20, 2);

    }

    private void Awake()
    {
        _playerInput = new PlayerInputControls();
        _playerInput.Enable();
        if (_playerInput != null)
        {
            _playerInput.PlayerActionMap.Movement.performed += MovementListener;
            _playerInput.PlayerActionMap.Movement.started += SlowDownTime;
            _playerInput.PlayerActionMap.Movement.canceled += MovementCanceled;
            _playerInput.PlayerActionMap.Movement.canceled += OriginalTime;

            _playerInput.PlayerActionMap.Aim.performed += AimListener;
        }
        _fromRotReflect = _reflector.transform;

    }

    private IEnumerator PlayerAnim()
    {
        transform.DOScale(_animScale, _animDuration).SetLoops(-1, LoopType.Yoyo);

        yield return null;

        //            DOColor(Color.yellow, 2).SetLoops(-1, LoopType.Yoyo);

    }
    private void Update()
    {
        //Debug.Log(Vector3.Dot(_rb.velocity.normalized, _inputVal.normalized));



        ParticleSpawn();

        MovePlayer();
        TimeManager._playerSpeed = _rb.velocity.magnitude / _maxVel;
        RotateReflector(_aimInputVector.x, _aimInputVector.y);

        _fromRotReflect = _reflector.transform;
        // Debug.Log(TimeManager.TimeScaling.global);
    }

    private void ParticleBreak()
    {
        //WORKING ON THIS LATER TWEAK THIS
        if (Vector3.Dot(_rb.velocity.normalized, _inputVal.normalized) <= -0.1f)
        {

            _ps = new ParticleSpawner(ParticleDirection.Constant, ParticleShape.Clouds).ChangeSize(0.4f, 0.8f).Activate(gameObject.transform).StopConstantAfterSeconds(0.5f);


            Debug.Log("play");



        }
    }

    private void ParticleSpawn()
    {
        ParticleHoverStandStill();
        ParticleBreak();
    }

    private void ParticleHoverStandStill()
    {
        if (Vector3.Dot(_rb.velocity.normalized, _inputVal.normalized) == 0)
        {

            _ps = new ParticleSpawner(ParticleDirection.Spreading, ParticleShape.Clouds).ChangeSize(0.5f, 1).StopConstantAfterSeconds(0.5f);
            StartCoroutine(PlayDelayedPS(_ps, 0.2F));
        }
        if (_inputVal != Vector2.zero)
        {
            _ps.StopConstant();
        }
    }

    private IEnumerator PlayDelayedPS(ParticleSpawner ps, float timeDelay)
    {

        yield return new WaitForSeconds(timeDelay);
        ps.Activate(gameObject.transform);

    }
    public void Pushback(Collision2D collision)
    {
        if (_inputVal == Vector2.zero)
        {
            Debug.Log("lol");
            _rb.AddForce(_reflector.transform.transform.right * _playerPushbackForce, ForceMode2D.Impulse);

        }
        else
        {
            _rb.AddForce(_reflector.transform.transform.right * (_playerPushbackForce / 2) * _rb.velocity.magnitude, ForceMode2D.Impulse);

        }


        //transform.DOShakePosition(_shakeDuration, _reflector.transform.transform.right * 0.5f, 1, 0, false, true);

    }

    private void SlowDownTime(CallbackContext callbackContext)
    {
        TimeManager.SlowDownTime();

    }
    private void OriginalTime(CallbackContext callbackContext)
    {
        TimeManager.OriginalTime();

    }
    /* private void TimeChange(CallbackContext callbackContext)
     {
         if (_rb.velocity != Vector2.zero)
         {
             TimeManager.SlowDownTime();
         }
         else
         {
             TimeManager.OriginalTime();
         }
     }
 */
    private void MovementListener(CallbackContext callbackContext)
    {
        _inputVal = callbackContext.ReadValue<Vector2>();
    }
    private void MovementCanceled(CallbackContext callbackContext)
    {
        _inputVal = Vector2.zero;
    }
    private void AimListener(CallbackContext callbackContext)
    {
        _aimInputVector = callbackContext.ReadValue<Vector2>();
    }
    private void AimCanceled(CallbackContext callbackContext)
    {
        _aimInputVector = Vector2.zero;
    }

    private void MovePlayer()
    {
        if (_rb.velocity.x < _maxVel || _rb.velocity.y < _maxVel || _rb.velocity.x > -_maxVel || _rb.velocity.y > -_maxVel)
        {
            if (_inputVal.x != 0 || _inputVal.y != 0)
                _rb.AddForce(new Vector2(_inputVal.x, _inputVal.y) * Time.deltaTime * _speed * TimeManager.TimeScaling.player / Time.timeScale);
        }




    }
    private void RotateReflector(float aimX, float aimY)
    {
        float angleDirection;
        float startAngle;



        Vector3 aim = -Vector3.up * aimX + Vector3.right * aimY;// new Vector2(aimX, aimY);
        /*  Debug.Log(aim);
          aim.Normalize();
          aim /= _reflectorDistance;
          _reflector.transform.localPosition = aim;*/
        float angle = Mathf.Atan2(aim.y, aim.x) * Mathf.Rad2Deg - 360;

        //Quaternion newRot = Quaternion.LookRotation(aim, -Vector3.forward);
        angle = Mathf.LerpAngle(angle, Mathf.Atan2(aim.x, -aim.y) * Mathf.Rad2Deg - 360, Time.time * _smoothnessAim);

        //   _reflector.transform.rotation = Quaternion.RotateTowards(_fromRotReflect.rotation, newRot, 3000 * Time.deltaTime);
        //_reflector.transform.rotation = Quaternion.Euler(_reflector.transform.rotation.eulerAngles.x, _reflector.transform.rotation.eulerAngles.y, 0);

        _reflector.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, angle));

    }
    public void UnsubscribeAllEvents()
    {
        _playerInput.PlayerActionMap.Movement.performed -= MovementListener;
        _playerInput.PlayerActionMap.Movement.started -= SlowDownTime;
        _playerInput.PlayerActionMap.Movement.canceled -= MovementCanceled;
        _playerInput.PlayerActionMap.Movement.canceled -= OriginalTime;
        _playerInput.Disable();
    }
    private void OnDestroy()
    {
        UnsubscribeAllEvents();
    }
}
