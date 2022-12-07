using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Restart : MonoBehaviour
{
    [SerializeField] GameObject button;
    /*void Update()
    {
        if(Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene("LowPolyFarmLite_Demo", LoadSceneMode.Single); // loads first scene
        }
    }*/

    private void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void RestartGame()
    {
        //button.SetActive(false);

        Initiate.Fade("LowPolyFarmLite_Demo", Color.white, 2.0f);
    }
}
