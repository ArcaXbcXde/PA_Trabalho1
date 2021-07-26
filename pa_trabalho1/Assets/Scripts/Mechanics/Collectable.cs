using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour {

	public float spinVelocity;

	public float reloadTime;

	private Transform interrogation;

	private float reloadCd;

	private void Start () {

		interrogation = transform.GetChild(0);
	}

	private void Update () {
		
		if (reloadCd < reloadTime) {

			reloadCd += Time.deltaTime;
		}
	}

	private void FixedUpdate () {

		transform.Rotate(Vector3.up * spinVelocity);
		interrogation.Rotate(Vector3.up * spinVelocity * -2);
	}

	private void OnTriggerEnter (Collider col) {

		if (col.tag == "Player" || col.tag == "AI") {

			interrogation.gameObject.SetActive(false);
			Invoke ("Reactivate", reloadTime);
			reloadCd = 0.0f;
		}
	}

	private void Reactivate () {
		
		interrogation.gameObject.SetActive(true);
	}
}