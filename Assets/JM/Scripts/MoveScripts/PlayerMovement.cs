using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]

public class PlayerMovement : MonoBehaviour
{
    #region Fields

    [Header("Movement")]
    [Tooltip("Used to readjust the player's forward direction based on the cam")]
    [SerializeField] private Transform orientation;
    [Tooltip("The player's move speed")]
    [SerializeField] private float moveSpeed;

    #endregion

    #region Members

    private ChickenAnimationController _cac;
    private Controls _playerControls; // Control script handles player input
    private InputAction _move; // Reads player input and outputs values
    private CharacterController _controller; // For player movement and terrain collision

    private float _horizontalInput;
    private float _verticalInput; 
    public bool IsRunning { get; private set; }

    private Vector3 _moveDirection;

    #endregion

    // Initialize player controls
    private void Awake()
    {
        _playerControls = new();
    }

    // Required step for InputAction
    private void OnEnable()
    {
        _move = _playerControls.Player.Move;
        _move.Enable();
    }

    // Required step for InputAction
    private void OnDisable()
    {
        _move.Disable();
    }

    private void Start()
    {
        _cac = GetComponentInChildren<ChickenAnimationController>();
        _controller = GetComponent<CharacterController>();
    }

    private void Update()
    {
        Input();
        MovePlayer();
    }

    /// <summary>
    /// Reads keys inputted by player in the _move InputAction
    /// Outputs value to corresponding members
    /// _move does not read from the y-axis since player is not expected
    /// to jump or leave the ground
    /// </summary>
    private void Input()
    {
        _horizontalInput = _move.ReadValue<Vector3>().x;
        _verticalInput = _move.ReadValue<Vector3>().z;
    }

    /// <summary>
    /// Calculates the direction the player is moving by comparing the
    /// corresponding inputs to the orientation variable
    /// </summary>
    private void MovePlayer()
    {
        _moveDirection =
            orientation.forward *
            _verticalInput +
            orientation.right *
            _horizontalInput;

        // Unless the player is standing still, the player is running
        IsRunning = _moveDirection.magnitude != 0;
        _cac.IsRunning = IsRunning;

        if (!_controller.isGrounded)
        {
            _moveDirection += Vector3.up * -9.81f;
        }

        // Execute Move method in the Character Controller
        _controller.Move(moveSpeed * Time.deltaTime * _moveDirection);

    }
    
}
