using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]

public class ChickMovement : MonoBehaviour
{
    #region Fields

    [ReadOnly]
    [SerializeField] float distance;

    [Tooltip("In-game player character")]
    public GameObject player;

    [SerializeField] GameObject raycastOrigin;

    [ReadOnly]
    [SerializeField] Vector3 velocity;

    [Tooltip("Chick behavior scriptable object Scripts/BehaviorObjects/ChickBehavior")]
    [SerializeField] ChickBehavior chickBehavior;

    #endregion

    #region Members

    private CharacterController _ctrl;
    private ChickenAnimationController _cac;
    private ChickFlock _flock;
    private PlayerMovement _pm;

    public Transform TargetTrans { get; set; }
    public bool CanFollow { get; private set; }
    public bool IsMoving { get; private set; }

    public float animSpeed;

    #endregion

    #region Methods

    private void Awake()
    {
        _ctrl = GetComponent<CharacterController>();
        _cac = GetComponentInChildren<ChickenAnimationController>();
        _flock = GetComponentInParent<ChickFlock>();
        _pm = player.GetComponent<PlayerMovement>();

        TargetTrans = player.transform;
        animSpeed = 0;
    }

    private void Start()
    {
        Physics.IgnoreCollision(player.GetComponent<Collider>(), GetComponent<Collider>()); 
    }

    private void Update()
    {
        velocity = _ctrl.velocity;
        distance = Vector3.Distance(
            transform.position,
            TargetTrans.position
        );

        // If the CanFollow boolean has been triggered by the player yet
        if (CanFollow)
        {
            // Rotate chick orientation to match with player
            chickBehavior.ChickRotate(
                TargetTrans,
                transform,
                raycastOrigin.transform
            );

            // Return whether the chick is running or not
            // and execute Character Controller's Move method
            // to move the chick
            IsMoving = chickBehavior.ChickMoving(
                _ctrl,
                _pm,
                _cac.anim,
                TargetTrans,
                transform,
                distance
            );

        }

        // If the player has not triggered CanFollow, check if the player
        // can trigger can follow
        else
        {
            CanFollow = distance <= chickBehavior.startFollowDistance;

            // Once CanFollow is triggered, instantiate the chick's
            // new follow target
            if (CanFollow)
            {
                TargetTrans = _flock.AssignTarget().transform;
            }
        }

        // Controls the chick run animation
        _cac.IsRunning = IsMoving;
    }

    /*public void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (!hit.collider.gameObject.CompareTag("Player"))
            return;

        GameObject obj = hit.collider.gameObject;

        Vector3 moveDir = transform.position - obj.transform.position;
        moveDir.y = 0;

        chickBehavior.Moving(
            _ctrl,
            moveDir,
            _ctrl.velocity
        );
    }*/

    #endregion
}
