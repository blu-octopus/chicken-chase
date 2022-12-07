using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ChaseBehavior/ChickBehavior")]

public class ChickBehavior : ChaseBehavior
{
    [ReadOnly]
    public string FilePath = "Assets/Scripts/BehaviorScripts/ChickBehavior";

    
    #region Methods

    /// <summary>
    /// Check conditions before moving chick and executing the running animation
    /// </summary>
    /// <param name="ctrl"> Chick's Character Controller component </param>
    /// <param name="pm"> Player movement component </param>
    /// <param name="chickTransform"> Chick's transform </param>
    /// <param name="distance"> Distance between chick and player </param>
    /// <returns> Boolean value that controls running animation </returns>
    public bool ChickMoving(
        CharacterController ctrl,
        PlayerMovement pm,
        Animator anim,
        Transform targetTrans,
        Transform thisTransform,
        float distance
    )
    {
        // If the player isn't moving, the chick should not execute
        // running animation despite the distance between the player
        // and chick
        if (
            distance > stopDistance &&
            !pm.IsRunning
        )
        {
            return false;
        }

        float animSpeed;

        // The chick should try to get as close to the player as
        // allowed by the stopDistance by calling ChaseBehavior's
        // Moving function
        if (distance > stopDistance)
        {
            Vector3 moveDir = targetTrans.position - thisTransform.position;
            moveDir.Normalize();

            animSpeed = Moving(
                ctrl,
                moveDir,
                true
            );
        }

        // If the chick is within stopDistance of the player, stop
        // running animation
        else
        {
            Vector3 moveDir = targetTrans.position - thisTransform.position;
            moveDir.Normalize();

            animSpeed = Moving(
                ctrl,
                moveDir,
                false
            );
        }

        anim.SetFloat("runSpeedMult", animSpeed);

        return animSpeed > 0;
    }

    /// <summary>
    /// Call ChaseBehavior Rotate method with the additional required
    /// parameter rotationSpeed
    /// </summary>
    /// <param name="targetTrans"></param>
    /// <param name="transform"></param>
    public void ChickRotate(
        Transform targetTrans,
        Transform transform,
        Transform raycastTrans
    )
    {
        Rotate(
            targetTrans,
            transform
        );

        CheckNormal(
            raycastTrans,
            transform
        );
    }

    #endregion
}
