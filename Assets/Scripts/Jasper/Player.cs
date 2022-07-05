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

    private void Start()
    {
        _playerInput = new PlayerInputControls();
        _playerInput.Enable();

        _rb.gravityScale = 0;

        _playerInput.PlayerActionMap.Movement.performed += MovementListener;
        _playerInput.PlayerActionMap.Movement.canceled += MovementCanceled;
        Rumbler.Instance.RumblePulse(0.5f, 1f, 20, 2);

    }
    private void FixedUpdate()
    {
        MovePlayer();

    }

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
    private void MovePlayer()
    {

        if (_inputVal.x != 0 || _inputVal.y != 0)
            _rb.AddForce(new Vector2(_inputVal.x, _inputVal.y) * _speed * Time.deltaTime);
    }

    private void RotateReflector()
    {
        Vector3 aim = new Vector3(_aimInputVector.x, 0, -_aimInputVector.y);
        aim.Normalize();
        aim /= _reflectorDistance;
        _reflector.transform.localPosition = aim;
    }

}
