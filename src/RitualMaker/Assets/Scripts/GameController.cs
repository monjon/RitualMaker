using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

	private bool PowerIsActivated = false;
	private string TypeOfPower;

	public GameObject FireParticle;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown (0)) {
			Debug.Log("Pressed left click.");
			if (PowerIsActivated) {
				Debug.Log("Display Power");
				switch (TypeOfPower) {
				case "Fire":
					Debug.Log ("Fire power displayed");
					// Get mouse clicked position in order to display the fx at that point
					Vector3 mousePos = Camera.main.ScreenToWorldPoint (Input.mousePosition);
					mousePos.z = 0;
					// Instantiate the prefab effect during runtime at the mouse position
					Instantiate (FireParticle, mousePos, Quaternion.identity);
					break;
				default:
					break;
				}
				PowerIsActivated = false;
			}
		}


		if (Input.GetMouseButtonDown (1)) {
			Debug.Log("Pressed right click.");
			PowerIsActivated = false;
		}


//		if (Input.GetMouseButtonDown (2)) {
//			Debug.Log("Pressed middle click.");
//		}

	}

	public void ActivatePower(string powerName){
		PowerIsActivated = true;
		TypeOfPower = powerName;
	}
}
