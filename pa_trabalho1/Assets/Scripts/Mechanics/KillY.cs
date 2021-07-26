using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillY : MonoBehaviour {

	private RaceManager raceManager;

	private void Start () {

		raceManager = RaceManager.Instance;
	}

	private void OnTriggerEnter (Collider col) {

		if (raceManager.runners.Contains(col.transform.parent)) {

			col.transform.position = raceManager.runnersCheckPoints[raceManager.runners.IndexOf(col.transform.parent)].transform.position;
		}
	}
}