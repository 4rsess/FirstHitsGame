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

    private GameObject currentTeleporter;

    void Update() {
        if (currentTeleporter != null && Input.GetKeyDown(KeyCode.E)) {
            LoadScene("TrainingLevel");
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Teleporter"))
        {
            currentTeleporter = collision.gameObject;
            Debug.Log("Вошел в зону телепортации.");

        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Teleporter"))
        {
            if (collision.gameObject == currentTeleporter)
            {
                currentTeleporter = null;
                Debug.Log("Вышел из зоны телепортации.");
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
        LoadScene("Lobby");
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
