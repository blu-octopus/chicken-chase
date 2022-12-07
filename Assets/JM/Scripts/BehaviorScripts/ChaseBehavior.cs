using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseBehavior : ScriptableObject
{
    [Header("Main Attributes for Chase Behavior")]

    [Tooltip("The distance between chaser and player where the chaser stops running")]
    public float stopDistance;

    [Tooltip("The distance the player needs to reach to trigger the chaser to follow")]
    public float startFollowDistance;

    [Tooltip("Chaser movement speed")]
    public float moveSpeed;

    public float maxSpeed;

    public float acceleration;

    public float deceleration;

    [Tooltip("Chaser rotation speed")]
    public float rotationSpeed;

    private const float Gravity = -9.81f;

    #region Methods

    /*public void Moving(
        CharacterController ctrl,
        Vector3 moveDir
    )
    {
        Vector3 velocity = ctrl.velocity;

        velocity += 2f * Time.deltaTime * moveDir;

        *//*velocity += velocity.magnitude < moveSpeed ?
            2f * Time.deltaTime * moveDir :
            Vector3.zero;*//*

        // Check if the player is not grounded and apply gravity as necessary
        if (!ctrl.isGrounded)
            velocity.y += Gravity * Time.deltaTime;

        // Execute character controller Move method to move object
        ctrl.Move(velocity * Time.deltaTime);
    }*/

    public float Moving(
        CharacterController ctrl,
        Vector3 moveDir,
        bool isAccel
    )
    {
        if ( isAccel )
        {
            moveSpeed = moveSpeed < maxSpeed ?
                moveSpeed + (acceleration * Time.deltaTime) :
                maxSpeed;
        }
        else
        {
            //moveSpeed = 0;
            moveSpeed = moveSpeed > 0 ?
                moveSpeed + (deceleration * Time.deltaTime) :
                0;
        }

        Vector3 moveCalc = moveSpeed * Time.deltaTime * moveDir;
        
        if (!ctrl.isGrounded)
        {
            moveCalc += Gravity * Time.deltaTime * Vector3.up;
        }

        ctrl.Move(moveCalc);

        return moveSpeed / maxSpeed;
    }

    /*public void Moving(
        CharacterController ctrl,
        Vector3 moveDir
    )
    {
        // Apply speed to the vector
        moveDir = moveSpeed * Time.deltaTime * moveDir;

        // Check if the player is not grounded and apply gravity as necessary
        if (!ctrl.isGrounded)
            moveDir += Gravity * Time.deltaTime * Vector3.up;

        // Execute character controller Move method to move object
        ctrl.Move(moveDir);
    }*/

    public void Rotate(
        Transform target,
        Transform objectTransform
    )
    {
        // Calculate the vector difference between object and player
        // Then set vector.y to 0 to prevent floating
        Vector3 direction = target.position - objectTransform.position;

        // Calculate object's required rotation so that it is facing the player
        Quaternion rotation = Quaternion.LookRotation(direction);

        // Apply rotation transform using Slerp
        objectTransform.rotation = Quaternion.Slerp(
            objectTransform.rotation,
            rotation,
            rotationSpeed * Time.deltaTime
        );
    }

    public void CheckNormal(
        Transform raycastTransform,
        Transform objectTransform
    )
    {
        if (Physics.Raycast(raycastTransform.position, Vector3.down, out RaycastHit hit, Mathf.Infinity))
        {
            Quaternion rotation = Quaternion.LookRotation(Vector3.Cross(objectTransform.TransformDirection(Vector3.right), hit.normal));

            objectTransform.rotation = Quaternion.Slerp(
                objectTransform.rotation,
                rotation,
                rotationSpeed * Time.deltaTime
            );
        }
    }

    #endregion
}
