using UnityEngine;
using System.Collections;

public class BubbleController : MonoBehaviour {

	public GameObject Icon;

	// Use this for initialization
	void Start () {

		GameObject go = (GameObject) GameObject.Instantiate (Icon);
		go.transform.position = new Vector3 (go.transform.position.x, go.transform.position.y, 0);
	}
	
	// Update is called once per frame
	void Update () {
//		Vector3 mousePos = Camera.main.ScreenToWorldPoint (Input.mousePosition);
//		mousePos.z = 0;
//
//		Debug.Log (mousePos);
//
//		GameObject go = (GameObject) GameObject.Instantiate (Icon, mousePos, Quaternion.identity);
//		go.transform.position = mousePos;
	}
}
