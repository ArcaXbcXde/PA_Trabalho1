using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour {

	[HideInInspector]
	public int checkpointNumber;

	// The target point for the AI to go for each checkpoint
	[HideInInspector]
	public Transform targetAI;
	
	private int runnerIndex;

	// the manager itself
	private RaceManager raceManager;

	public delegate void RunnerLap (int runnerNumber);
	public RunnerLap doLap;

	public delegate void RunnerCheckpoint (int runnerNumber, Checkpoint thisCheckpoint);
	public RunnerCheckpoint newCheckpoint;

	private void Awake () {

		targetAI = transform.GetChild(0);
	}

	private void Start () {

		raceManager = RaceManager.Instance;
	}

	private void OnTriggerEnter (Collider col) {

		Runner thisRunner = col.GetComponent<Runner>();

		if (thisRunner != null && checkpointNumber != 0 && newCheckpoint != null) {

			newCheckpoint(thisRunner.runnerNumber, this);
		} else if (thisRunner != null && checkpointNumber == 0 && doLap != null) {

			doLap(thisRunner.runnerNumber);
		}

		/* Implemented as delegate in the race manager

			// If the runner's actual checkpoint is the previous one, set this as the new checkpoint
			if ((raceManager.runnersCheckPoints[runnerIndex].checkpointNumber) == (checkpointNumber - 1)) {

				raceManager.runnersCheckPoints[runnerIndex] = this;

			// Else if this is the starter checkpoint and the runner's actual checkpoint is the last, set this as the new checkpoint and add a lap
			} else if ((checkpointNumber == 0) && ((raceManager.runnersCheckPoints[runnerIndex].checkpointNumber) == (raceManager.checkpoints.Length - 1))) {

				raceManager.runnersCheckPoints[runnerIndex] = this;
				raceManager.lapCount[runnerIndex] ++;
			}*/
	}
}