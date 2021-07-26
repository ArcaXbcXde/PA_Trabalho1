using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitIdle : StateMachineBehaviour {

	private AIAgent runner;

	public string actionTrigger;

	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {

		runner = animator.GetComponent<AIAgent>();
	}
	
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        
		if (RaceManager.Instance.raceStarted == true || runner.agent.destination != null) {
			animator.SetTrigger (actionTrigger);
		}
    }
}