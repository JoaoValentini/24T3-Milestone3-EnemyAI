using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    bool isPaused = false;
    public bool IsPaused => isPaused;
    [SerializeField] GameObject pausePanel;
    [SerializeField] GameObject losePanel;
    void Start()
    {
       LockCursor();
    }


    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
            TogglePause();
    }

    public void TogglePause()
    {
        isPaused = !isPaused;
        pausePanel.SetActive(isPaused);
        if(isPaused)
        {
            UnlockCursor();
            Time.timeScale = 0;
        }
        else
        {
            LockCursor();
            Time.timeScale = 1;
        }
    }

    public void GoToMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("MainMenu");
    }

    void LockCursor()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
    void UnlockCursor()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void EndGame()
    {
        if(losePanel.activeInHierarchy)
            return;
        Time.timeScale = 0;
        losePanel.SetActive(true);
        UnlockCursor();

    }
}
