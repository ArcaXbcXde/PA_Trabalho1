using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToNextCheckpoint : StateMachineBehaviour {

	public string blendTreeValue;

	private AIAgent runner;

	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {

		runner = animator.GetComponent<AIAgent>();
	}

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {

		if (RaceManager.Instance.raceStarted == true
			&& ((runner.agent.destination == null)
			|| (Vector3.Distance (runner.agent.destination, runner.transform.position) < 1)
			|| (runner.agent.isStopped == true)
			//|| ((runner.race.checkpoints[runner.race.runnersCheckPoints[runner.runnerNumber + 1].checkpointNumber].targetAI.position != runner.targetDestination) && (runner.race.checkpoints[0].targetAI.position != runner.targetDestination))
			// && runner.raceBegan == true
			)) {

			runner.agent.SetDestination (runner.targetDestination);
			//Debug.Log("statemachine destination: " + runner.targetDestination);
		}

		animator.SetFloat (blendTreeValue, runner.agent.velocity.magnitude);
    }
	
}