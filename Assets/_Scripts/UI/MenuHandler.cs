using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuHandler : MonoBehaviour
{
    [SerializeField]
    private GameObject MainMenu;
    [SerializeField]
    private GameObject PauseMenu;
    [SerializeField]
    private GameObject ControlsMenu;
    [SerializeField]
    private GameObject WinScreen;
    [SerializeField]
    private GameObject LoseScreen;

    public static MenuHandler Instance;

    private void Awake()
    {
        Instance = this;

        Pause();
        MainMenu.SetActive(true);
        PauseMenu.SetActive(false);
        ControlsMenu.SetActive(false);
    }

    public void Pause()
    {
        Time.timeScale = 0;
        Cursor.visible = true;
    }

    public void Play()
    {
        Time.timeScale = 1;
        Cursor.visible = false;
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void Restart()
    {
        SceneManager.LoadScene(0);
    }

    public void ToggleControls()
    {
        ControlsMenu.SetActive(!ControlsMenu.activeSelf);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseMenu.SetActive(!PauseMenu.activeSelf);

            if (PauseMenu.activeSelf)
            {
                Pause();
            }
            else
            {
                Play();
            }
        }
    }

    public void Win()
    {
        Pause();
        WinScreen.SetActive(true);
    }

    public void Lose()
    {

    }
}
