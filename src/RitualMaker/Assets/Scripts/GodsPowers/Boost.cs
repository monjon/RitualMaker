using UnityEngine;
using System.Collections;

public class Boost : MonoBehaviour {

	public GameController gc;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	public void PowerActivated () {
		Debug.Log ("Change mouse cursor");
		gc.ActivatePower ("Boost");
	}
}
