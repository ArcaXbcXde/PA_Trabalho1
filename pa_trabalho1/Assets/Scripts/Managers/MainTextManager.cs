using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainTextManager : MonoBehaviour {
	
	[Tooltip ("Time to wait the race start")]
	[Range (0.0f, 30.0f)]
	public float countdown = 5.0f;
	
	private bool countdownEnded = false;

	private Text myText;

	public static MainTextManager Instance {

		get;
		private set;
	}

    private void Awake() {

		InitialSet();
		myText = GetComponent<Text>();
    }
	
	private void Update () {

		if (countdownEnded == false) {

			if (countdown > 0f) {

				countdown -= Time.deltaTime;
				myText.text = (countdown + 0.6f).ToString("f0");
			} else {

				countdownEnded = true;
				RaceManager.Instance.raceStarted = true;
				myText.text = "Go!";
				Invoke("Deactivate", 3);
			}
		}

		/*
		if (countdown < 5.5) {

			countdown = Time.realtimeSinceStartup;
			myText.text = (5.5 - countdown).ToString("f0");
		} else {

			countdown = 7;
			RaceManager.Instance.raceStarted = true;
			myText.text = "Go!";
			Invoke("Deactivate", 3);
		}*/
	}

	// Mark this as its own instance
	private void InitialSet () {

		if (Instance == null) {

			Instance = this;
		}
	}

	// Make the text appear at the screen
	public void Activate () {

		gameObject.SetActive(true);
	}

	// Make the text not appear at the screen
	public void Deactivate () {

		gameObject.SetActive(false);
	}

	// Change what is appearing at the screen
	public void ChangeText(string newText) {

		myText.text = newText;
	}
}