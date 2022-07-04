using UnityEngine;
using static UnityEngine.InputSystem.InputAction;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float _speed;

    [SerializeField]
    private Rigidbody2D _rb;

    private PlayerInput _playerInput;

    private Vector2 _inputVal;



    private void Start()
    {
        _playerInput = new PlayerInput();
        _playerInput.Enable();


        _playerInput.PlayerActionMap.Movement.performed += MovementListener;
    }
    private void FixedUpdate()
    {
        MovePlayer();
    }

    private void MovementListener(CallbackContext callbackContext)
    {
        Debug.Log("test");
        _inputVal = callbackContext.ReadValue<Vector2>();
    }

    private void MovePlayer()
    {
        _rb.AddForce(new Vector2(_inputVal.x, _inputVal.y) * _speed * Time.deltaTime);
    }
}
