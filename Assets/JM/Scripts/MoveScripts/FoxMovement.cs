using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoxMovement : MonoBehaviour
{
    #region Fields

    [SerializeField] float distance;

    public GameObject player;

    [SerializeField] LayerMask groundMask;

    [SerializeField] GameObject raycastOrigin;

    [Tooltip("Chick behavior scriptable object Scripts/BehaviorObjects/FoxBehavior")]
    [SerializeField] FoxBehavior foxBehavior;

    #endregion

    #region Members

    private CharacterController _ctrl;
    private FoxAnimationController _fac;

    public bool CanFollow { get; set; }

    #endregion

    // Start is called before the first frame update
    void Start()
    {
        _ctrl = GetComponent<CharacterController>();
        _fac = GetComponentInChildren<FoxAnimationController>();
        
        CanFollow = false;
    }

    // Update is called once per frame
    void Update()
    {
        distance = Vector3.Distance(
            transform.position,
            player.transform.position
        );

        if (CanFollow)
        {
            _fac.IsRunning = foxBehavior.FoxMoving(
                _ctrl,
                player.transform,
                transform,
                _fac.anim,
                distance
            );

            foxBehavior.FoxRotate(
                player.transform,
                transform,
                raycastOrigin.transform
            );
            
        }
        else
        {
            CanFollow = distance < foxBehavior.startFollowDistance;
        }
        
        
    }

    //private void CheckNormal()
    //{
    //    if (Physics.Raycast(raycastOrigin.transform.position, Vector3.down, out RaycastHit hit, Mathf.Infinity))
    //    {
    //        Quaternion rotation = Quaternion.LookRotation(Vector3.Cross(transform.TransformDirection(Vector3.right), hit.normal));

    //        transform.rotation = Quaternion.Slerp(
    //            transform.rotation,
    //            rotation,
    //            foxBehavior.rotationSpeed * Time.deltaTime
    //        );
    //    }
    //}
}
