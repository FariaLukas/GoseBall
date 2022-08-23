using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    private bool isPaused;
    public GameObject PauseScreen;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
        PauseScreen.SetActive(isPaused);
    }

    public void TogglePause()
    {
        isPaused = !isPaused;
       
        Time.timeScale = isPaused ? 0 : 1;
    }
}
