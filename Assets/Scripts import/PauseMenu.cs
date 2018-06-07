using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour {
    public static bool isPaused;
    public GameObject backdrop;
    public Paddle player;
    public BrickSpawner spawner;
    public Button resumeButton;
    public Button restartButton;
    public Button exitButton;

	// Use this for initialization
	void Start () {
        SetMenuState(false);

        resumeButton.onClick.AddListener(OnResumePressed);
        restartButton.onClick.AddListener(OnRestartPressed);
        exitButton.onClick.AddListener(OnExitPressed);
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SetMenuState(!backdrop.activeSelf);
        }
	}

    void SetMenuState(bool _active)
    {
        backdrop.SetActive(_active);
        Time.timeScale = _active ? 0 : 1;
        isPaused = _active;

        //Cursor.lockState = _active ? CursorLockMode.None : CursorLockMode.Locked;
    }

    void OnResumePressed()
    {
        SetMenuState(false);
    }

    void OnRestartPressed()
    {
        player.Reset();
        spawner.Reset();

        SetMenuState(false);
    }

    void OnExitPressed()
    {
        Debug.Log("This will quit the game in build i promise");
        Application.Quit();
    }
}
