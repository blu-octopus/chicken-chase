using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ThirdPersonCamera : MonoBehaviour
{
    [Header("Camera References")]
    [SerializeField] Transform orientation;
    [SerializeField] Transform player;
    [SerializeField] Transform playerObject;

    [Space]

    [SerializeField] float rotationSpeed;

    public Transform combatLookAt;
    private Controls _playerControls;
    private InputAction _move; 

    public CameraStyle currStyle;
    public enum CameraStyle
    {
        Basic,
        Combat
    }

    private void Awake()
    {
        _playerControls = new();
    }

    private void OnEnable()
    {
        _move = _playerControls.Player.Move;
        _move.Enable();
    }

    private void OnDisable()
    {
        _move.Disable();
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    private void Update()
    {
        // rotate orientation
        Vector3 viewDir =
            player.position - new Vector3(
            transform.position.x,
            player.position.y,
            transform.position.z
        );
        orientation.forward = viewDir.normalized;

        float horizontalInput = _move.ReadValue<Vector3>().x;
        float verticalInput = _move.ReadValue<Vector3>().z;

        Vector3 inputDir =
            orientation.forward *
            verticalInput +
            orientation.right *
            horizontalInput;

        if (inputDir != Vector3.zero)
        {
            playerObject.forward = Vector3.Slerp(
                playerObject.forward,
                inputDir.normalized,
                Time.deltaTime * rotationSpeed
            );
        }              
    }
}
