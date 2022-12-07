using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Score : MonoBehaviour
{
    [Tooltip("The flock game object that holds the script")]
    [SerializeField] GameObject flock;

    [Tooltip("TMPro UI element that will display the score")]
    [SerializeField] TextMeshProUGUI scoreText;

    private ScoreTracker _scoreTracker;

    // Start is called before the first frame update
    void Start()
    {
        _scoreTracker = flock.GetComponent<ScoreTracker>();
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.SetText($"{_scoreTracker.score} / {_scoreTracker.maxScore}");
    }
}
