using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ChaseBehavior/FoxBehavior")]

public class FoxBehavior : ChaseBehavior
{
    [ReadOnly]
    public string FilePath = "Assets/Scripts/BehaviorScripts/FoxBehavior";


    public bool FoxMoving(
        CharacterController ctrl,
        Transform targetTransform,
        Transform transform,
        Animator anim,
        float distance
    )
    {
        if (distance > stopDistance)
        {
            Vector3 dir = targetTransform.position - transform.position;
            dir.Normalize();

            float animSpeed = Moving(
                ctrl,
                dir,
                true
            );

            anim.SetFloat("runSpeedMult", animSpeed);

            return animSpeed > 0;
        }
        else
        {
            return false;
        }
    }

    public void FoxRotate(
        Transform targetTransform,
        Transform transform,
        Transform raycastOrigin
    )
    {
        Rotate(
            targetTransform,
            transform
        );

        CheckNormal(
            raycastOrigin,
            transform
        );
    }
}
