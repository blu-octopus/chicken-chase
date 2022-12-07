using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GunRotate : MonoBehaviour
{
    [SerializeField] Transform orientation;
    [SerializeField] GameObject player;
    [SerializeField] float rotationSpeed;

    private Controls _playercontrols;
    private InputAction _inputAction;

    private void Awake()
    {
        _playercontrols = new();
    }

    private void OnEnable()
    {
        _inputAction = _playercontrols.Player.Move;
        _inputAction.Enable();
    }

    private void OnDisable()
    {
        _inputAction.Disable();
    }

    // Update is called once per frame
    void Update()
    {
        Quaternion rotation = player.transform.rotation;
        transform.rotation = rotation;
    }

}

