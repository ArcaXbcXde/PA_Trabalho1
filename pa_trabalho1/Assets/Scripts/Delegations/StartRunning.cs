using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartRunning : MonoBehaviour {

	private Runner runner;

	private void Awake () {
		runner = GetComponent<Runner>();
		if (runner == true) {

			runner.raceHasStarted += RaceBegin;
		}
	}

	public void RaceBegin () {

		RaceManager.Instance.raceStarted = true;
	}
}