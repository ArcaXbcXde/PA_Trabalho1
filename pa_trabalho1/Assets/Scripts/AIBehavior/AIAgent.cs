using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIAgent : Runner {

	[HideInInspector]
	public Vector3 targetDestination;

	[HideInInspector]
    public NavMeshAgent agent;

	private float baseSpeed;

	private TerrainModifier currentTerrain = TerrainModifier.Air;

	[HideInInspector]
	public RaceManager race;

	private void Awake () {
		
		agent = GetComponent<NavMeshAgent>();

		baseSpeed = agent.speed;
	}

	private void Start () {
		
		race = RaceManager.Instance;
	}

	private void Update () {
		
		if ((RaceManager.Instance.raceStarted == true
			&& (agent.destination == null 
			|| Vector3.Distance (agent.destination, transform.position) < 1) 
			//|| (agent.destination != race.checkpoints[race.runnersCheckPoints[runnerNumber + 1].checkpointNumber + 1].targetAI.position && agent.destination != race.checkpoints[0].targetAI.position)
			)) {

			if (race.runnersCheckPoints[runnerNumber].checkpointNumber + 1 < race.checkpoints.Length) {
				
				targetDestination = (race.checkpoints[race.runnersCheckPoints[runnerNumber].checkpointNumber + 1].targetAI.position);
			} else {

				targetDestination = (race.checkpoints[0].targetAI.position);
			}
		}
		
		if (leftFoot.terrainType != currentTerrain) {
			
			currentTerrain = leftFoot.terrainType;
			agent.speed = baseSpeed * ((float)currentTerrain / 100);
		}
		if (rightFoot.terrainType != currentTerrain) {

			currentTerrain = rightFoot.terrainType;
			agent.speed = baseSpeed * ((float) currentTerrain / 100);
		}
	}
}