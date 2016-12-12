using UnityEngine;

public class PauseMenuBehaviour : MainMenuBehaviour
{
    public static bool isPaused;

    public GameObject pauseMenu;

	private GameObject _spawnPoint;
	private GameObject _player;

    public void Start()
    {
		this._spawnPoint = GameObject.FindWithTag("SpawnPoint");
		this._player = GameObject.FindWithTag ("Player");
        isPaused = false;
        pauseMenu.SetActive(false);
    }

    public void Update()
    {
        if (Input.GetKeyUp("escape"))
        {
                OpenPauseMenu();
        }
    }

    public void ResumeGame()
    {
        isPaused = false;
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
    }

    public void RestartGame()
    {
        Application.LoadLevel(Application.loadedLevelName);
    }

    public void OpenPauseMenu()
    {
        pauseMenu.SetActive(true);
    }

	public void GoToCheckPoint()
	{
		_player.transform.position = _spawnPoint.transform.position;
	}

}
