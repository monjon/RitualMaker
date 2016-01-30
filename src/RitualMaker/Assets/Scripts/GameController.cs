using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

	// Singleton Logic
	//

	static private GameController instance;
	static public GameController Instance
	{
		get {return GameController.instance;}
	}

	public void Awake(){

		// Singleton logic 
		if(GameController.instance == null){
			GameController.instance = this;
			GameObject.DontDestroyOnLoad(this.gameObject);
		}

	}

	// Properties
	//

	// For now, used to prevent bad ui behaviour
	private float timerPowerUsed = 0.1f;
	public bool IsPowerReady {
		get { return this.timerPowerUsed < 0f;}
	}

	// Methods
	//

	private bool PowerIsActivated = false;
	public bool IsPowerActive {
		get {return this.PowerIsActivated;}
	}
	private string TypeOfPower;
	public string ActivePowerID{
		get {return this.TypeOfPower;}
	}

	public GameObject FireParticle;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		// Cooldown for power activated (TMP used for UI)
		if(!this.PowerIsActivated){
			this.timerPowerUsed -= Time.deltaTime;
		}

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
					GameObject go = (GameObject) GameObject.Instantiate (FireParticle, mousePos, Quaternion.identity);
					Destroy (go, 2);

					break;
				default:
					break;
				}
				PowerIsActivated = false;
				this.timerPowerUsed = 0.1f;
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
