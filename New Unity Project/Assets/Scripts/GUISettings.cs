using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GUISettings : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadSceneAsync("gamescenes_1");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
