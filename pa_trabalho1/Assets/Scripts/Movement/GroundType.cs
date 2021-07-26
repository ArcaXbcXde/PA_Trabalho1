using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TerrainModifier {
	Ice = 150,
	Asphalt = 100,
	Stone = 75,
	Grass = 50,
	Sand = 45,
	HighGrass = 30,
	Water = 25,
	Mud = 10,
	Air = 100,
}

public class GroundType : MonoBehaviour {
    
	public TerrainModifier groundType;
}
