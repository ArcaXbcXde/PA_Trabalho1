using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBase : MonoBehaviour {

	public LayerMask targetLayers;

	public PowerUpBase myPowerUp;

	private Vector3 velocity;
	
	private float gravity;

	private Rigidbody rigid;

	private void Awake () {
		
		rigid = GetComponent<Rigidbody>();

		velocity = myPowerUp.velocity;

		gravity = myPowerUp.gravity;

	}

	private void Update () {
		
	}

	private void OnTriggerEnter (Collider col) {
		
	}
}