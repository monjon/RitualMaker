using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Spot : MonoBehaviour {

	// Properties
	//

	public List<Path> Paths = new List<Path>();

	[Header("Ressources")]
	public bool HasMinerals = false;
	public bool HasFood = false;
	public bool HasIntel = false;


	// Methods
	//

	public Path GetPathToSpot(){

		if(this.Paths.Count > 0){
			// Return a random path to this spot
			return this.Paths[Random.Range(0, this.Paths.Count -1)];

		}
		else{
			Debug.Log("Spot.GetPathToSpot - No paths found !");
			return null;
		}

	}


}
