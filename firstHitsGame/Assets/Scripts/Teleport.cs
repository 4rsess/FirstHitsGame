using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;

public class Teleport : MonoBehaviour
{

    private void LoadScene(string nameScene)
    {
        PlayerPrefs.SetString("oldScene", PlayerPrefs.GetString("nowScene"));

        PlayerPrefs.SetString("nowScene", nameScene);
        if (nameScene != "LevelOne1")
            SceneManager.LoadScene(nameScene);
        else
        {
            SceneManager.LoadScene("LevelOne");
        }
    }

    private GameObject currentTeleporter;

    void Update()
    {
        if (currentTeleporter != null && Input.GetKeyDown(KeyCode.E))
        {
            LoadScene(currentTeleporter.name);
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Teleporter"))
        {
            currentTeleporter = collision.gameObject;
            Debug.Log("1");

        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Teleporter"))
        {
            if (collision.gameObject == currentTeleporter)
            {
                currentTeleporter = null;
                Debug.Log("1");
            }
        }
    }

    public void RestartLevel()
    {
        LoadScene(PlayerPrefs.GetString("oldScene"));
        Time.timeScale = 1f;
    }

    public void MainMenu()
    {
        LoadScene("StartMenu");
    }

    public void NewGame()
    {
        PlayerPrefs.SetString(PlayerPrefs.GetString("LevelOne"), "Co");
        LoadScene("Lobby");
    }

    public void QuitGame()
    {
        Debug.Log("1");
        Application.Quit();

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }

}
