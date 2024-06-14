using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Teleport : MonoBehaviour
{
    private void LoadScene(string nameScene) {
        PlayerPrefs.SetString("oldScene", PlayerPrefs.GetString("nowScene"));
        PlayerPrefs.SetString("nowScene", nameScene);
        SceneManager.LoadScene(nameScene);
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && currentTeleporter!=null) 
        {
            LoadScene(currentTeleporter.name);
        }
    }

    private GameObject currentTeleporter;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Teleporter"))
        {
            currentTeleporter = collision.gameObject;
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Teleporter"))
        {
            if (collision.gameObject == currentTeleporter)
            {
                currentTeleporter = null;
            }
        }
    }

    public void RestartLevel() 
    {
        LoadScene(PlayerPrefs.GetString("OldScene"));
    }

    public void MainMenu()
    {
        LoadScene("StartMenu");
    }

    public void NewGame()
    {
        LoadScene("StartNewGameLevel");
    }

    public void QuitGame()
    {
        Debug.Log("игра закрыта");
        Application.Quit();

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }

}
