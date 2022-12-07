using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class' only purpose is to keep track of how many chicks were saved
/// </summary>
public class ScoreTracker : MonoBehaviour
{
    [Tooltip("The flock object that holds all chicks as children")]

    public int score;
    public int maxScore;

    private void Start()
    {
        score = 0;
        maxScore = transform.childCount;
    }
}
