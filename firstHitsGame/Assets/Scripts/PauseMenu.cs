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
            else {
                Resume();
            }
        }
    }

    public void Resume()
    {
        Debug.Log("������� ���� �����");
        pauseGameMenu.SetActive(false);
        
        PauseGame = false;
    }

    public void Pause()
    {
        Debug.Log("������ ���������");
        pauseGameMenu.SetActive(true);
        
        PauseGame = true;
    }

    public void LoadMenu()
    {
        
        SceneManager.LoadScene("StartMenu");
    }

  
}
