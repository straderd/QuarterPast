using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLogic : MonoBehaviour
{
    [SerializeField] Canvas gameOverCanvas;
    [SerializeField] float invokeDelay = 0.75f;
    
    void Start()
    {
        gameOverCanvas.enabled = false;
    }

    public void GameOverTriggered()
    {
        gameOverCanvas.enabled = true;
    }

    public void ReloadScene()
    {
        FindObjectOfType<AudioController>().PlayMenuSelect();
        Invoke("ReloadSceneInvoked", invokeDelay);
    }

    private void ReloadSceneInvoked()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
    }

    public void QuitApplication()
    {
        FindObjectOfType<AudioController>().PlayMenuSelect();
        Invoke("QuitApplicationInvoked", invokeDelay);
    }

    private void QuitApplicationInvoked()
    {
        Application.Quit();
    }

    public void LoadNextScene()
    {
        FindObjectOfType<AudioController>().PlayMenuSelect();
        Invoke("LoadNextSceneInvoked", invokeDelay);
    }

    private void LoadNextSceneInvoked()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.buildIndex + 1);
    }

    public void LoadMainMenu()
    {
        FindObjectOfType<AudioController>().PlayMenuSelect();
        Invoke("LoadMainMenuInvoked", invokeDelay);
    }

    private void LoadMainMenuInvoked()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.buildIndex - 2);
    }
}
