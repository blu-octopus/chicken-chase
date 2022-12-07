using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;

public class ChangeCamController : MonoBehaviour
{
    [SerializeField] private InputAction cameraSwitch;
    [SerializeField] private CinemachineFreeLook TPCombat;
    [SerializeField] private CinemachineFreeLook TPFree;

    private int prio;

    private void OnEnable()
    {
        cameraSwitch.Enable();
    }

    private void OnDisable()
    {
        cameraSwitch.Disable();
    }

    private void Start()
    {
        prio = TPFree.Priority;
    }

    // Update is called once per frame
    void Update()
    {
        TPCombat.Priority =
            cameraSwitch.IsPressed() ?
            prio + 1:
            prio - 1;
    }
}
