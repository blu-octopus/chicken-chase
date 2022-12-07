using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class test : MonoBehaviour
{
    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        Debug.Log(hit.gameObject.tag);
        if (hit.gameObject.tag == "Fox")
        {
            SceneManager.LoadScene("GameOver");
        }
    }
}
