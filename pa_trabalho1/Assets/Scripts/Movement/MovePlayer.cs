using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlayer : Runner {

	#region public variables
	
	[Header("Movement")]

	[Tooltip("Maximum speed the player can run")]
	[Range(5.0f, 50.0f)]
	public float maxSpeed = 20.0f;

	[Tooltip("Strength in which the character moves forward")]
	[Range(1.0f, 100.0f)]
	public float forwardMoveStr = 5.0f;

	[Tooltip("Strength in which the character moves backward")]
	[Range(1.0f, 100.0f)]
	public float backwardMoveStr = 2.5f;

	[Tooltip("the speed in which the player can turn")]
	[Range(1.0f, 1080.0f)]
	public float rotateSpeed = 180.0f;

	public float lowerIdleSeconds = 4.0f;

	public float upperIdleSeconds = 6.0f;

	[Header("Jump")]

	[Tooltip("If the character can jump or not")]
	public bool jumpable = true;

	[Tooltip("Jump strength")]
	[Range(1.0f, 1000.0f)]
	public float jumpStr = 100.0f;

	[HideInInspector]
	public bool canMove = true;

	#endregion

	#region private variables

	// Made to easily get if the player is on ground
	[HideInInspector]
	public bool onGround = false;

	// Buffer on Update to use to jump on the next FixedUpdate
	private bool jumpBuffer = false;

	// Used to define what idle animation was chosen
	private int idleTypeChosen = 1;

	// The random time it choose to do the next idle animation
	private float idleTimeChosen;

	// The amount of time without inputs
	private float idleTime = 0.0f;

	// For strength buff/debuff changes and be able to go back to the original move strength
	private float originalMoveStr;

	// The calculation result using the player max speed for the max speed allowed on the current terrain
	private float terrainMaxSpeed;

	// Basic unity axis control variable, easily used for animations
	private float movX, movZ;

	// The amount of strength calculated after vertical input, used for making the characters walk
	private float inputStr;

	// The amount of strength calculated after horizontal input, used for spinning the characters
	private float inputTurn;

	// Strength to be applied in the player's rigidbody
	private Vector3 moveStrengthApplied;

	// The player's rigidbody
	private Rigidbody rigid;

	// The player's animator
	private Animator anima;
	
	#endregion

	#region basic methods

	// Mainly to initialize variables
	private void Awake () {

		rigid = GetComponent<Rigidbody>();

		anima = GetComponent<Animator>();
		
		originalMoveStr = forwardMoveStr;

		idleTimeChosen = UnityEngine.Random.Range(lowerIdleSeconds, upperIdleSeconds);
	}

	// Mainly to control player inputs and countdowns
	private void Update () {
		
		if (onGround == true && RaceManager.Instance.raceStarted == true) {

			CheckInputs();
		} else {

			NoInputs();
		}
	}

	// Mainly to control character actions
	private void FixedUpdate () {

		//CheckGroundRaycast();

		CheckGroundTrigger();

		// Movement checks
		if (canMove == true) {
			if (onGround == true) {

				CheckWalk();

				CheckJump();
			}

			if (moveStrengthApplied != new Vector3(0, 0, 0)) {

				DoMovement();
			}
		}

	}

	// Mainly to control animations
	private void LateUpdate () {
		
		CheckAnimations();
	}

	#endregion

	#region check methods

	private void CheckGroundTrigger () {

		if (leftFoot.ctGround > 0 || rightFoot.ctGround > 0) {

			onGround = true;
		} else {

			onGround = false;
		}
	}

	/* Use the built-in unity axis to check if the player have done any input,
	 * if the input was positive or negative
	 * then treats it accordingly by calculating the resulting strength or action of each input 
	 */
	private void CheckInputs () {

		// Front or Back
		CheckVerticalInput();

		// Left or Right
		CheckHorizontalInput();

		// If jumped
		CheckJumpInput();
	}

	// Check vertical axis
	private void CheckVerticalInput () {
		movZ = Input.GetAxis("Vertical");

		if (Input.GetKey(KeyCode.LeftShift) == true) {

			movZ /= 1.5f;

			if (rigid.velocity.magnitude > 10.0f) {

				rigid.velocity = rigid.velocity.normalized * 10.0f;
			}
		}

		if (movZ > 0) {

			inputStr = movZ * forwardMoveStr;
		} else if (movZ < 0) {

			inputStr = movZ * backwardMoveStr;
		}
	}

	// Check horizontal axis
	private void CheckHorizontalInput () {

		movX = Input.GetAxis("Horizontal");

		inputTurn = movX * ((1 / maxSpeed) * rigid.velocity.magnitude) * rotateSpeed * Time.deltaTime;

		transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles + new Vector3(0.0f, inputTurn, 0.0f));
	}

	/* Simple method to see if the character has input any walk strength
	 * and the "terrainSpeedModifier" average which is got by the feet, calculate
	 * and apply it on "moveStrengthApplied" x position
	 */
	private void CheckWalk () {

		if (Mathf.Abs(inputStr) > 0) {

			moveStrengthApplied = transform.forward * inputStr;
		}
	}

	// Check jump axis
	private void CheckJumpInput () {

		if ((jumpable == true) && (Math.Abs(Input.GetAxis("Jump")) > 0.1f)) {

			jumpBuffer = true;
		}
	}

	/* Simple method to see if the character has input any jump command
	 * by checking if the jump buffer was changed in the last Update() update
	 * and jump on the next fixedUpdate() update
	 */
	private void CheckJump () {

		if (jumpBuffer == true) {

			moveStrengthApplied += new Vector3(0, jumpStr, 0);
			jumpBuffer = false;
		}
	}

	private void CheckAnimations () {

		// If the player isn't doing any action, the character will idle
		if ((Math.Abs(movZ) < 0.1f) && (Math.Abs(movX) < 0.1f) && (Math.Abs(Input.GetAxis("Jump")) < 0.1f)) {

			idleTime += Time.deltaTime;
			if (idleTime > idleTimeChosen) {

				if (idleTypeChosen <= 45) {

					anima.SetTrigger("OtherIdle");
					idleTime = 0.0f;
					Invoke("DoingAnAction", 5);

				} else if (idleTypeChosen <= 98) {

					anima.SetTrigger("AngryIdle");
					idleTime = 0.0f;

				} else if (idleTypeChosen > 98) {

					anima.SetTrigger("DanceIdle");
					idleTime = 0.0f;
				}

				idleTimeChosen = UnityEngine.Random.Range(lowerIdleSeconds, upperIdleSeconds);
				idleTypeChosen = UnityEngine.Random.Range(0, 101);
			}
		} else {

			anima.SetTrigger("Action");
			idleTime = 0.0f;
		}
		if (onGround == true) {
			
			anima.SetFloat("MovZ", rigid.velocity.magnitude);
		} else {
			// Falling animation
			// ↓ Should be temporary ↓
			anima.SetFloat("MovZ", (rigid.velocity.magnitude - rigid.velocity.y));
		}
	}

	// Just to avoid coroutines
	private void DoingAnAction () {

		anima.SetTrigger("Action");
		idleTime = 0.0f;
	}

	#endregion

	#region do methods

	/* Simple method to apply "moveStrengthApplied" force
	 * on the Rigidbody and make the character move,
	 * then zeroes it
	 */
	private void DoMovement () {

		rigid.AddForce(moveStrengthApplied);

		terrainMaxSpeed = maxSpeed * (((float) leftFoot.terrainType + (float) rightFoot.terrainType) / 200);

		if (rigid.velocity.magnitude > terrainMaxSpeed) {

			rigid.velocity = rigid.velocity.normalized * terrainMaxSpeed;
		}
		moveStrengthApplied = new Vector3(0, 0, 0);

	}

	// If there is no inputs at the moment resets all movement-related variables
	private void NoInputs () {

		movX = 0;

		movZ = 0;

		inputStr = 0;
		
		jumpBuffer = false;
	}
	#endregion

	#region special methods

	// Used to change the player move strength on the run
	public void SetMoveStr (float newStr, float changeTime) {
		forwardMoveStr = newStr;
		Invoke("ReturnMoveStr", changeTime);
	}

	// Created to avoid the use of corroutines
	private void ReturnMoveStr () {
		forwardMoveStr = originalMoveStr;
	}

	#endregion
}