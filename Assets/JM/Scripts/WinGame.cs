using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinGame : MonoBehaviour
{
    [SerializeField] ScoreTracker scoreTracker;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player") &&
            scoreTracker.score == scoreTracker.maxScore
        )
        {
            Initiate.Fade("WinScreen", Color.white, 2.0f);
            //SceneManager.LoadScene("WinScreen", LoadSceneMode.Single);
        }
    }
}
