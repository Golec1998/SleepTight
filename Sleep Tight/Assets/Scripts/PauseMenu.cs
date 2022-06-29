using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class PauseMenu : MonoBehaviour
{
    
    public static bool gameIsPaused = false;
    public static bool canPause = true;
    public GameObject pauseMenuUI;
    public GameObject gameUI;
    public GameObject camera;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(canPause)
            {
                if (gameIsPaused)
                    Resume();
                else
                    Pause();
            }
        }
    }

    public void Resume()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        pauseMenuUI.SetActive(false);
        gameUI.SetActive(true);
        Time.timeScale = 1f;
        camera.GetComponent<CinemachineFreeLook>().enabled = true;
        gameIsPaused = false;
    }

    void Pause()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        pauseMenuUI.SetActive(true);
        gameUI.SetActive(false);
        Time.timeScale = 0f;
        camera.GetComponent<CinemachineFreeLook>().enabled = false;
        gameIsPaused = true;
    }

    public void Restart()
    {
        Debug.Log("Sam se prze³aduj :(");
    }

    public void Menu()
    {
        Debug.Log("Te¿ bym chcia³ wróciæ...");
    }
}
