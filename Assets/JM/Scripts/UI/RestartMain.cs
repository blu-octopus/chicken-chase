using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RestartMain : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI[] elements;
    [SerializeField] GameObject button;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void RestartGame()
    {
        foreach (var element in elements)
        {
            element.alpha = 0;
        }

        button.SetActive(false);

        Initiate.Fade("LowPolyFarmLite_Demo", Color.white, 2.0f);
    }
}
