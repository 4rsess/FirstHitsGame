using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public bool PauseGame;
    public GameObject pauseGameMenu;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!PauseGame)
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        Debug.Log("Покинул меню паузы");
        pauseGameMenu.SetActive(false);
        Time.timeScale = 1f;

        PauseGame = false;
    }

    public void Pause()
    {
        Debug.Log("Нажата шестерёнка");
        pauseGameMenu.SetActive(true);
        Time.timeScale = 0f;

        PauseGame = true;
    }

    public void LoadMenu()
    {
        Time.timeScale = 1f;

        SceneManager.LoadScene("StartMenu");
    }

  
}
