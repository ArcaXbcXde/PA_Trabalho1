using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompleteLap : MonoBehaviour {
	
	private RaceManager raceManager;

	private void Start () {

		raceManager = RaceManager.Instance;

		Checkpoint runnerLap = GetComponent<Checkpoint>();
		if (runnerLap == true) {

			runnerLap.doLap += AddLap;
		}
	}

	/* Called only by the starting checkpoint
	 * indicating that the runner reached it,
	 * and if the last checkpoint is the runner's,
	 * turns the second one as the runner's and complete a lap
	 */
	public void AddLap (int runnerNumber) {
		if (raceManager != null) {

			if ((raceManager.runnersCheckPoints[runnerNumber].checkpointNumber) == (raceManager.checkpoints.Length - 1)) {

				raceManager.runnersCheckPoints[runnerNumber] = raceManager.checkpoints[1];
				raceManager.CountLap(runnerNumber);
			}
		}
	}
}