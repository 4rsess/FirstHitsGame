using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ListLevels : MonoBehaviour
{
    int levelUnLock;
    public Button[] buttons;
    [SerializeField] private GameObject[] lockImages;

    void Start()
    {
        levelUnLock = PlayerPrefs.GetInt("levels", 1);

        for (int i = 0; i < buttons.Length; i++)
        {
            if (i < levelUnLock)
            {
                buttons[i].interactable = true;
                lockImages[i].SetActive(false);
            }
            else
            {
                buttons[i].interactable = false;
                lockImages[i].SetActive(true);
            }
        }
    }

    public void LoadLevels(int levelIndex)
    {
        SceneManager.LoadScene(levelIndex);
    }
}
