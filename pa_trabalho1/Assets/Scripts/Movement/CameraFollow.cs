using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {
	
	#region public variables

	[Tooltip ("The holder which the camera will follow")]
	public Transform player;

	[Header("Movement")]

	[Tooltip ("Speed which the mouse travels through the screen")]
	[Range (1.0f, 20.0f)]
	public float sensitivity = 5.0f;

	[Tooltip ("Makes mouse movimentation smoother")]
	[Range (1.0f, 50.0f)]
	public float smoothness = 10.0f;

	[Tooltip ("The highest angle the camera can pan")]
	[Range (0.0f, 90.0f)]
	public float cameraUpperBound = 80.0f;

	[Tooltip ("The lowest angle the camera can pan")]
	[Range(-90.0f, 0.0f)]
	public float cameraLowerBound = -20.0f;

	[Tooltip ("Invert the camera horizontal axis")]
	public bool invertCameraX = false;

	[Tooltip ("Invert the camera vertical axis")]
	public bool invertCameraY = false;

	[Header("Zoom")]

	[Tooltip ("Amount of zoom applied each mousewheel spin")]
	[Range(1.0f, 20.0f)]
	public float zoomStrength = 2.0f;

	[Tooltip ("Makes the zoom smoother")]
	[Range(1.0f, 50.0f)]
	public float zoomSmoothness = 5.0f;

	[Tooltip ("Makes the zoom stronger while further and weaker while nearer")]
	[Range (0.0f, 1.0f)]
	public float zoomIntensity = 0.333333f;

	[Tooltip ("The CLOSEST the camera can get from the player")]
	[Range (0.0f, 100.0f)]
	public float zoomMax = 1.0f;

	[Tooltip ("The FURTHEST the camera can get from the player")]
	[Range(0.0f, 100.0f)]
	public float zoomMin = 10.0f;

	[Tooltip ("Invert the side to scrool to zoom in or out")]
	public bool invertZoom;

	#endregion

	#region private variables

	private float camX, camY;

	// Camera target zoom distance
	private float zoomValue;

	private float scroll;

	// Camera local rotation
	private Vector3 thisLocalRotation;

	// Amount rotacioned in the actual frame
	private Quaternion frameRotation;

	// The transform of the camera holder
	private Transform holderTransform;

	#endregion

	#region basic methods

	private void Awake () {

		holderTransform = transform.parent;

		zoomValue = transform.position.z;
	}

	private void LateUpdate () {

		holderTransform.position = player.transform.position;

		MouseHandle();

		RotationHandle();

		ZoomHandle();
	}

	#endregion

	#region handlers methods

	/* Simple method to handle mouse movement
	 * and set respective variables,
	 * if mouse is not locked
	 */
	private void MouseHandle () {

		if (Cursor.lockState == CursorLockMode.Locked) {

			camX = Input.GetAxisRaw("Mouse X");
			camY = Input.GetAxisRaw("Mouse Y");

			scroll = -Input.GetAxis("Mouse ScrollWheel");
		} else {

			camX = camY = 0;
		}
	}
	
	/* Calculate and rotate the camera
	 * based on mouse movement and some parameters
	 * to limit how much the camera can pan
	 * and interpolate smoothly
	 */
	private void RotationHandle () {

		if (camX != 0 || camY != 0) {

			// Gets mouse movement
			if (invertCameraX == false) {

				thisLocalRotation.x += camX * sensitivity;
			} else {

				thisLocalRotation.x -= camX * sensitivity;
			} 

			if (invertCameraY == false) {

				thisLocalRotation.y -= camY * sensitivity;
			} else {

				thisLocalRotation.y += camY * sensitivity;
			}

			// Clamp camera pan at the y axis
			thisLocalRotation.y = Mathf.Clamp(thisLocalRotation.y, cameraLowerBound, cameraUpperBound);
		}

		// Get camera target rotation
		frameRotation = Quaternion.Euler(thisLocalRotation.y, thisLocalRotation.x, 0.0f);

		// Interpolation between the actual rotation and the target rotation using "smoothness" value to make the rotation smooth
		holderTransform.rotation = Quaternion.Lerp(holderTransform.rotation, frameRotation, smoothness * Time.deltaTime);
	}
	
	/* Calculate and apply zoom in the camera
	 * based on mousewheel scroll movement and others parameters
	 * to limit up to where the camera can zoom
	 * and interpolate smoothly
	 */
	private void ZoomHandle () {

		if (scroll != 0.0f) {
			
			// Amount of zoom to apply
			if (invertZoom == false) {

				zoomValue += scroll * zoomStrength * zoomValue * zoomIntensity;
			} else {

				zoomValue -= scroll * zoomStrength * zoomValue * zoomIntensity;
			}

			// Clamp zoom
			zoomValue = Mathf.Clamp(zoomValue, -zoomMin, -zoomMax);
		}

		if (transform.localPosition.z != -zoomValue) {

			// Interpolation between the actual zoom position and the target zoom position using "zoomSmoothness" value to make zooming smooth
			transform.localPosition = Vector3.forward * Mathf.Lerp(transform.localPosition.z, zoomValue, zoomSmoothness * Time.deltaTime);
		}
	}

	#endregion
}