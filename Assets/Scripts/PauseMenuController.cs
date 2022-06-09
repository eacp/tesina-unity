using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenuController : MonoBehaviour
{
    private bool paused = false;

    [Tooltip("XD")]
    [SerializeField]
    private GameObject pausePanel;

    /// <summary>
    /// Toggle pause. This will alternate between paused and
    /// not paused.
    /// </summary>
    /// <param name="value"></param>
    void OnPauseMenu(UnityEngine.InputSystem.InputValue value)
    {
        paused = !paused;
        Debug.Log("PAUSED PRESSED");

        // Execute some actions depending on wether we are paused
        // or not

        if(paused) pause();
        else unpause();
    }

    private void pause()
    {
        // Set the timescale to zero
        Time.timeScale = 0f;

        // Enable the pause panel
        pausePanel.SetActive(true);
    }

    private void unpause()
    {
        // Reset the timescale
        Time.timeScale = 1f;

        // Disable the pause panel
        pausePanel.SetActive(false);
    }
}
