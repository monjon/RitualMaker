using UnityEngine;
using System.Collections;

public class Fire : MonoBehaviour {

	public GameController gc;

	// Use this for initialization
	void Start () {
	
	}
	
	public void PowerActivated(){
		Debug.Log ("Change mouse cursor");
		gc.ActivatePower ("Fire");
	}

	var duration : float = 1.0;
	var alpha : float = 0;

	void Update(){

		lerpAlpha();
	}

	void lerpAlpha () {

		var lerp : float = Mathf.PingPong (Time.time, duration) / duration;

		alpha = Mathf.Lerp(0.0, 1.0, lerp) ;
		renderer.material.color.a = alpha;
	}
}
