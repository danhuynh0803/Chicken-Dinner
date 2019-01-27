using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuEvents : MonoBehaviour
{
    #region Singleton
    public static MenuEvents instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    #endregion

    public GameObject GameOverMenu;
    public GameObject PauseMenu;
    public GameObject WinMenu; 

    private Fading fadeController;
    private bool isGamePaused; 

    private void Start()
    {
        isGamePaused = false;
        if (GameOverMenu)
            GameOverMenu.SetActive(false);
        if (PauseMenu)
            PauseMenu.SetActive(false);
        if (WinMenu)
            WinMenu.SetActive(false);

        fadeController = FindObjectOfType<Fading>();
        Resume();
    }

    private void Update()
    {        
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isGamePaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    IEnumerator ChangeScene(string sceneName)
    {
        float fadeTime = fadeController.BeginFade(1);
        yield return new WaitForSeconds(fadeTime);
        SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
    }

    IEnumerator ChangeScene(int sceneIndex)
    {
        float fadeTime = fadeController.BeginFade(1);
        yield return new WaitForSeconds(fadeTime);
        SceneManager.LoadScene(sceneIndex, LoadSceneMode.Single);
    }

    public void Pause()
    {
        Time.timeScale = 0.0f;
        isGamePaused = true;
        if (PauseMenu)
            PauseMenu.SetActive(true);
    }

    public void Resume()
    {
        Time.timeScale = 1.0f;
        isGamePaused = false;
        if (PauseMenu)
            PauseMenu.SetActive(false);
        if (GameOverMenu)
            GameOverMenu.SetActive(false);
    }

    public void OpenWinPanel()
    {
        if (WinMenu)
            WinMenu.SetActive(true);
    }

    public void RestartScene()
    {
        Resume();
        StartCoroutine(ChangeScene(SceneManager.GetActiveScene().buildIndex));      
    }

    // Assumes the main menu scene will be index 0
    public void ReturnToMainMenu()
    {
        Time.timeScale = 1.0f;
        StartCoroutine(ChangeScene(0));
    }

    public void LoadNextScene()
    {
        // Move to next level based on current scene index
        LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void LoadScene(int sceneIndex)
    {
        StartCoroutine(ChangeScene(sceneIndex));
    }

    public void LoadScene(string sceneName)
    {
        StartCoroutine(ChangeScene(sceneName));
    }
}
