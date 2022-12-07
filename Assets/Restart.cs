using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Restart : MonoBehaviour
{
    [SerializeField] GameObject button;

    [SerializeField] TextMeshProUGUI text;

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
        button.SetActive(false);

        text.SetText("Loading...");

        Initiate.Fade("LowPolyFarmLite_Demo", Color.white, 2.0f);
    }
}
