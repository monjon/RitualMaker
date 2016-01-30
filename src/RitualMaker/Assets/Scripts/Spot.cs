using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Spot : MonoBehaviour {

	// Properties
	//

	public List<Path> Paths = new List<Path>();

	// Methods
	//

	public void GetPathToSpot(){

		if(this.Paths.Count > 0){
			
		}
		else{
			Debug.Log("Spot.GetPathToSpot - No paths found !");
		}

	}


}
