using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Runner : MonoBehaviour {

	// The runner's left foot
	public FeetCollision leftFoot;

	// The runner's right foot
	public FeetCollision rightFoot;
	
	// Used as an runner id
	[HideInInspector]
	public int runnerNumber;

	public delegate void VictoryStatus();
	public VictoryStatus raceHasStarted;
	public VictoryStatus runnerWin;
	public VictoryStatus runnerLose;

}