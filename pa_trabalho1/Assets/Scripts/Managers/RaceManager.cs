using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RaceManager : MonoBehaviour {

	[Tooltip ("Total amount of laps required to end race")]
	[Range (1, 10)]
	public int lapsToComplete = 3;

	[Tooltip ("Time to restart the scene after the player has reached the end of the race")]
	[Range (3.0f, 120.0f)]
	public float waitToEndRace = 10.0f;
	
	[Tooltip ("Object that holds every runner")]
	public Transform runnersHolder;

	[Tooltip ("Pull all checkpoints in the course, in order")]
	public Checkpoint[] checkpoints;

	//[Header ("Hidden Values")]

	// If the race has started
	[HideInInspector]
	public bool raceStarted;

	// If the game is paused or not
	[HideInInspector]
	public bool paused = false;

	// Lists all runners
	[HideInInspector]
	public List<Transform> runners;

	// Lists in what checkpoint each runner is
	[HideInInspector]
	public Checkpoint[] runnersCheckPoints;

	// Lists in what lap each runner is
	[HideInInspector]
	public int[] lapCount;

	private int countFinished = 1;

	private bool playerFinished;

	public static RaceManager Instance {

		get;
		private set;
	}

	private void Awake () {

		InitialSet();
	}
	
	private void Start () {
		
		for (int i = 0; i < runnersHolder.childCount; i++) {
																																
			runners.Add (runnersHolder.GetChild(i).GetChild(0));
			if (runners[i].GetComponent<Runner>() == true) {

				runners[i].GetComponent<Runner>().runnerNumber = i;
			}
		}

		for (int i = 0; i < checkpoints.Length; i++) {

			checkpoints[i].checkpointNumber = i;
		}
		
		lapCount = new int[runners.Count];
		runnersCheckPoints = new Checkpoint[runners.Count];

		for (int i = 0; i < runnersCheckPoints.Length; i++) {

			runnersCheckPoints[i] = checkpoints[0];
		}
	}

	// Mark this as its own instance
	private void InitialSet () {

		if (Instance == null) {

			Instance = this;
		}
	}
	
	/* Called when someone completes a lap
	 * to verify if its the player and end the race,
	 * if its not the player makes the runner stop running
	 * and count as another that the player is behind at
	 */
	public void CountLap (int runnerNumber) {
		
		lapCount[runnerNumber]++;

		if (lapCount[runnerNumber] >= lapsToComplete) {

			if (runners[runnerNumber].GetComponent<MovePlayer>() != null) {

				playerFinished = true;
				MainTextManager.Instance.ChangeText(countFinished + "º\nplace!");
				MainTextManager.Instance.Activate();
				Invoke("EndRace", waitToEndRace);
			} else if (playerFinished == false) {

				countFinished++;
				runners[runnerNumber].GetComponent<Runner>().enabled = false;
			}
		}
	}

	private void EndRace () {

		GetComponent<FadeManager>().StartFadeIn();
		GetComponent<SceneControl>().RestartSceneWithTimer (1 / GetComponent<FadeManager>().fadeSpd);
	}
}