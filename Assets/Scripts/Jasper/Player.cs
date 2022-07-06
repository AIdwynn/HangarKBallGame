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



    private void Start()
    {



        _rb.gravityScale = 0;
        // UnsubscribeAllEvents();


        _aimInputVector = new Vector2(-1, 0);

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


    }
    private void Update()
    {
        /* if (_TESTSLOW)
         {
             TimeManager.ChangeTime();

             _TESTSLOW = false;
         }*/
        // TimeChange();
        MovePlayer();
        RotateReflector(_aimInputVector.x, _aimInputVector.y);
        // Debug.Log(TimeManager.TimeScaling.global);
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




        Debug.Log(_rb.velocity);
    }
    private void RotateReflector(float aimX, float aimY)
    {
        float angleDirection;
        float startAngle;



        Vector2 aim = new Vector2(aimX, aimY);

        aim.Normalize();
        aim /= _reflectorDistance;
        _reflector.transform.localPosition = aim;


        /* Vector2 mp = Camera.main.ScreenToWorldPoint(aim);

         Vector2 dir = mp - (Vector2)transform.position;

         angleDirection = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

         startAngle = 360 + angleDirection;*/

        float angle = Mathf.Atan2(aim.y, aim.x) * Mathf.Rad2Deg - 360;

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
