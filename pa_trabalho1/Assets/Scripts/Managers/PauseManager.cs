using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseManager : MonoBehaviour {

	private RaceManager gameManager;
	private SceneControl sceneControl;

	private GameObject pauseScreen;
	private GameObject optionsScreen;

	private void Start () {
		
		gameManager = RaceManager.Instance;
		sceneControl = SceneControl.Instance;

		pauseScreen = transform.GetChild(0).gameObject;
		optionsScreen = transform.GetChild(1).gameObject;
	}

	private void Update () {

		CheckPause();
	}

	private void CheckPause () {

		if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P)) {

			TogglePause();
		}
	}

	public void TogglePause () {

		gameManager.paused = !gameManager.paused;

		if (gameManager.paused == false) {

			UnpauseGame();
		} else {

			PauseGame();
		}
	}

	public void PauseGame () {

		Time.timeScale = 0;
		gameManager.paused = true;
		pauseScreen.SetActive(true);
		sceneControl.UnlockCursor();
	}

	public void UnpauseGame () {

		Time.timeScale = 1;
		gameManager.paused = false;
		optionsScreen.SetActive(false);
		pauseScreen.SetActive(false);
		sceneControl.LockCursor();
	}
}