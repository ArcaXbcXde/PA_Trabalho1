using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeetCollision : MonoBehaviour {
	
	public int ctGround = 0;

	public TerrainModifier terrainType;

	public ParticleSystem particles;
	
	private void OnTriggerEnter (Collider col) {
		
		if (col.tag == "Ground") {

			//Checks 
			if (col.GetComponent<GroundType>() != null) {

				terrainType = col.GetComponent<GroundType>().groundType;
			} else {
				terrainType = TerrainModifier.Air;
			}
			
#pragma warning disable CS0618 // O tipo ou membro é obsoleto
			particles.startColor = col.GetComponent<Renderer>().material.color;
#pragma warning restore CS0618 // O tipo ou membro é obsoleto
			
			particles.Play();
			ctGround ++;
		}
	}

	private void OnTriggerExit (Collider col) {

		if (col.tag == "Ground") {
			ctGround --;
			if (ctGround <= 0) {
				ctGround = 0;
				particles.Stop();
			}
		}
	}
}