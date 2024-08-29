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

    public void PauseButton()
    {
        Time.timeScale = 0; 
        SceneManager.LoadSceneAsync("ResumeMenu", LoadSceneMode.Additive);
    }

    public void ResumeGame()
    {
        StartCoroutine(ResumeGameCoroutine());
    }

    private IEnumerator ResumeGameCoroutine()
    {
        AsyncOperation unloadOperation = SceneManager.UnloadSceneAsync("ResumeMenu");
        while (!unloadOperation.isDone)
        {
            yield return null;
        }

        Time.timeScale = 1.0f; 
    }

}
