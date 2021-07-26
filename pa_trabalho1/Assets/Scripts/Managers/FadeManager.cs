using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeManager : MonoBehaviour {

	[HideInInspector]
	public bool fadeIn = false;

	[HideInInspector]
	public bool fadeOut = false;

	[Tooltip ("The speed in which the fade will occur (percentage of transparency per second)")]
	[Range(0.01f, 4.0f)]
	public float fadeSpd = 0.5f;

	[Tooltip ("The black screen that will cover the screen during fades")]
	public Image blackScreen;
	
	// The variable that will alter the black screen alpha
	private Color alphaColor;
	
	private void Awake () {
		
		alphaColor = blackScreen.color;
	}

	private void OnEnable () {

		alphaColor.a = 1.0f;
		StartFadeOut();
	}

	public void Update () {


		if (fadeIn == true) {

			if (blackScreen.gameObject.activeSelf == false) {

				blackScreen.gameObject.SetActive(true);
			}
			FadeIn();
			
			// Enquanto a tela não está completamente transparente
		} else if (fadeOut == true){

			FadeOut();
		}
		
	}
	
	// Start to show a black screen
	public void StartFadeIn () {

		alphaColor.a = 0.0f;
		fadeIn = true;
	}

	// Start to hide the black screen
	public void StartFadeOut () {

		alphaColor.a = 1.0f;
		fadeOut = true;
	}

	// Make the black screen appear
	private void FadeIn () {

		alphaColor.a += Time.deltaTime * fadeSpd;
		blackScreen.color = alphaColor;
		if (alphaColor.a >= 1) {

			fadeIn = false;
		}
	}

	// Make the black screen disappear
	private void FadeOut () {

		alphaColor.a -= Time.deltaTime * fadeSpd;
		blackScreen.color = alphaColor;
		if (alphaColor.a <= 0) {

			fadeOut = false;
			blackScreen.gameObject.SetActive(false);
		}
	}
}