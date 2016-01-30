using UnityEngine;
using System.Collections;

public class Fire : MonoBehaviour {

	public GameController gc;

	private float duration = 1.0f;
	private float alpha = 0f;

	// Use this for initialization
	void Start () {
	
	}
	
	public void PowerActivated(){
		Debug.Log ("Change mouse cursor");
		gc.ActivatePower ("Fire");
	}

	void Update(){

		lerpAlpha();
	}

	void lerpAlpha () {

		float lerp = Mathf.PingPong (Time.time, duration) / duration;

		alpha = Mathf.Lerp(0.0f, 1.0f, lerp) ;
		GetComponent<Renderer>().material.color.a = alpha;
	}
}
