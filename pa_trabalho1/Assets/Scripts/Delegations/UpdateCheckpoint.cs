using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateCheckpoint : MonoBehaviour {
	
	private RaceManager raceManager;

	private void Start () {

		raceManager = RaceManager.Instance;

		Checkpoint runnerLap = GetComponent<Checkpoint>();
		if (runnerLap == true) {

			runnerLap.newCheckpoint += NextCheckpoint;
		}
	}



	/* If the runner's checkpoint is the previous as the one that called this method,
	 * turns the actual as the new runner's
	 */
	public void NextCheckpoint (int runnerNumber, Checkpoint runnerCheckpoint) {

		if ((raceManager.runnersCheckPoints[runnerNumber].checkpointNumber) == (runnerCheckpoint.checkpointNumber - 1)) {

			raceManager.runnersCheckPoints[runnerNumber] = runnerCheckpoint;
		}
	}
}