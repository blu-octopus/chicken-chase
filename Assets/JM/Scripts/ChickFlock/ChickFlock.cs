using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This class oversees all chicks that are following the player.
// Primarily focuses on instantiating new follow targets for
// newly acquired chicks and maintains spacing between these
// targets to prevent clumping
public class ChickFlock : MonoBehaviour
{
    #region Fields

    [Tooltip("Spacing between targets")]
    [SerializeField] float updateSpacing;

    [Tooltip("Distance around the player where a target may spawn")]
    [SerializeField] float playerRadius;

    [Tooltip("In-game player character")]
    [SerializeField] GameObject player;

    [Tooltip("Chick target prefab")]
    [SerializeField] GameObject targetPrefab;

    #endregion

    #region Members

    [Tooltip("Holds all targets")]
    public List<GameObject> chickTargets;

    private ScoreTracker _scoreTracker;

    #endregion

    private void Start()
    {
        _scoreTracker = GetComponent<ScoreTracker>();
    }

    private void Update()
    {
        foreach (GameObject go in chickTargets)
        {
            foreach (GameObject goo in chickTargets)
            {
                // Move to the next iteration if comparing against itself
                if (goo == go)
                    continue;

                // Get the distance between the two targets
                float dist = Vector3.Distance(
                    go.transform.position,
                    goo.transform.position
                );

                // If the distance between two targets is less than
                // the required spacing, move this target away from
                // the compared target
                if (dist < updateSpacing)
                {
                    Vector3 diff =
                        goo.transform.position -
                        go.transform.position;

                    // Space out targets over time to optimize spacing
                    goo.transform.position += Time.deltaTime * diff;
                }
            }
        }
    }

    // Instantiate a new target for the most recent chick to follow
    public GameObject AssignTarget()
    {
        // Output a random x value within the player radius
        float x = Random.Range(
            player.transform.position.x - playerRadius,
            player.transform.position.x + playerRadius
        );

        // Output a random z value within the player radius
        float z = Random.Range(
            player.transform.position.z - playerRadius,
            player.transform.position.z + playerRadius
        );

        // Construct a new vector position for the target using
        // the randomly generated x and z values
        Vector3 newPos = new(x, player.transform.position.y, z);

        // Instantiate target and parent it with the player
        GameObject newTarget = Instantiate(
            targetPrefab,
            newPos,
            player.transform.rotation,
            player.transform
        );

        // Rename target for distinction and add to target list
        newTarget.name = "ChickTarget_" + chickTargets.Count;

        // Increment score
        _scoreTracker.score++;

        chickTargets.Add(newTarget);

        return newTarget;
    }

    private void ResolveSpacing(GameObject newObj)
    {
        foreach (GameObject go in chickTargets)
        {
            // Move to the next iteration if comparing against itself
            /*if (goo == go)
                continue;*/

            // Get the distance between the two targets
            float dist = Vector3.Distance(
                go.transform.position,
                newObj.transform.position
            );

            // If the distance between two targets is less than
            // the required spacing, move this target away from
            // the compared target
            if (dist < updateSpacing)
            {
                Vector3 diff =
                    newObj.transform.position -
                    go.transform.position;

                // Space out targets over time to optimize spacing
                newObj.transform.position += Time.deltaTime * diff;
            }
        }
    }
}
