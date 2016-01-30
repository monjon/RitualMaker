using UnityEngine;
using System.Collections;

public class Lightning : MonoBehaviour {

	public GameController gc;

	// Use this for initialization
	void Start () {
	
	}
	
	public void PowerActivated(){
		Debug.Log ("Change mouse cursor");
		gc.ActivatePower ("Lightning");
	}

}
