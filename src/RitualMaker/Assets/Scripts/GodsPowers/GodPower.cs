using UnityEngine;
using System.Collections;

public class GodPower : MonoBehaviour {

	// Properties
	//

	public string PowerID;

	[Tooltip("If true, FearLove = -1, else 1")]
	public bool isFearfull= false;

	public int FearLove{
		get {
				if(this.isFearfull){
					return 1;
				}
				else{
					return -1;
				}
			}
	}

	public float Range = 5f;

	// Methods
	//
}
