using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpBase : ScriptableObject {

	public Vector3 velocity = new Vector3 (0.0f, 0.0f, 0.0f);

	public float gravity = 10.0f;

	public bool useOnSelf;
}