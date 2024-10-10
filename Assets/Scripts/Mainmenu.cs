using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Mainmenu : MonoBehaviour
{

    public void OnPlayButton()
    {
        SceneManager.LoadScene("Gameplay");
    }
    public void OnQuitButton()
    {
        Application.Quit();
    }
}
