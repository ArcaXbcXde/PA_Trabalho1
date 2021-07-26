using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneControl : MonoBehaviour {

	public static SceneControl Instance {

		get;
		private set;
	}

	#region basic methods

	private void Awake () {

		InitialSet();
	}

	// Guarantee the correct initial state of the scene
	private void Start () {
		
		LockCursor();

		Time.timeScale = 1;

		RaceManager.Instance.paused = false;
	}

	private void Update () {
		
		CheckCursorLock ();
	}

	#endregion

	// Mark this as its own instance
	private void InitialSet () {

		if (Instance == null) {

			Instance = this;
		}
	}

	#region manage cursor

	// Check if the cursor lock key was pressed
	private void CheckCursorLock () {

		if (Input.GetKeyDown(KeyCode.Tab)) {

			if (Cursor.lockState == CursorLockMode.Locked) { // unlock cursor

				UnlockCursor();
			} else if (Cursor.lockState == CursorLockMode.None) { // lock cursor

				LockCursor();
			}
		}
	}

	// Make the cursor invisible and unable to move
	public void LockCursor () {

		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;
	}

	// Make the cursor visible and able to move
	public void UnlockCursor () {

		Cursor.lockState = CursorLockMode.None;
		Cursor.visible = true;
	}

	#endregion

	#region manage scenes

	// Call to restart the scene after "timer" seconds
	public void RestartSceneWithTimer (float timer) {
		Invoke("RestartScene", timer);
	}

	// Change the actual scene to "sceneName" scene
	public void ChangeTo (string sceneName) {

		Cursor.lockState = CursorLockMode.None;
		Cursor.visible = true;
		SceneManager.LoadScene(sceneName);
	}

	// Restart the scene
	public void RestartScene () {

		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}

	// Exit the game
	public void QuitGame () {

		Application.Quit();
	}

	#endregion
}