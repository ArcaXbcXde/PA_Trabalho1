using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraOptions : MonoBehaviour {
	
	public Text cameraSensitValue;

	public Text cameraSmoothValue;

	public Text zoomSensitValue;

	public Text zoomSmoothValue;

	public Slider cameraSensit;

	public Slider cameraSmooth;

	public Slider zoomSensit;

	public Slider zoomSmooth;

	public Toggle invertCameraX;

	public Toggle invertCameraY;

	public Toggle invertZoom;

	private CameraFollow cameraScript;

    private void Start() {

        cameraScript = Camera.main.GetComponent<CameraFollow>();

		cameraSensitValue.text = cameraScript.sensitivity.ToString("f0");
		cameraSmoothValue.text = cameraScript.smoothness.ToString("f0");
		zoomSensitValue.text = cameraScript.zoomStrength.ToString("f0");
		zoomSmoothValue.text = cameraScript.zoomSmoothness.ToString("f0");
	}

	public void ChangeCameraSensitivity () {

		cameraScript.sensitivity = cameraSensit.value;
		cameraSensitValue.text = cameraSensit.value.ToString("f0");
	}

	public void ChangeCameraSmooth () {

		cameraScript.smoothness = cameraSmooth.value;
		cameraSmoothValue.text = cameraSmooth.value.ToString("f0");
	}

	public void ChangeZoomSensitivity () {

		cameraScript.zoomStrength = zoomSensit.value;
		zoomSensitValue.text = zoomSensit.value.ToString("f0");
	}

	public void ChangeZoomSmooth () {

		cameraScript.zoomSmoothness = zoomSmooth.value;
		zoomSmoothValue.text = zoomSmooth.value.ToString("f0");
	}

	public void InvertCameraX () {

		cameraScript.invertCameraX = invertCameraX.isOn;
	}

	public void InvertCameraY () {

		cameraScript.invertCameraY = invertCameraY.isOn;
	}

	public void InvertZoom () {

		cameraScript.invertZoom = invertZoom.isOn;
	}
}