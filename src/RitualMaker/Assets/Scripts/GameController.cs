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

	private bool PowerIsActivated = false;
	public bool IsPowerActive {
		get {return this.PowerIsActivated;}
	}
	private string TypeOfPower;
	public string ActivePowerID{
		get {return this.TypeOfPower;}
	}

	public GameObject FireParticle;
	public GameObject LightningParticle;
	public GameObject BoostParticle;

	[Header("Power Range")]
	public float FireRange = 1f;
	public float LightningRange = 1f;

	// Methods
	//
	
	// Update is called once per frame
	void Update () {

		// Cooldown for power activated (TMP used for UI)
		if(!this.PowerIsActivated){
			this.timerPowerUsed -= Time.deltaTime;
		}

		if (Input.GetMouseButtonDown (0)) {
			Debug.Log("Pressed left click.");
			if (PowerIsActivated) {
				// Get mouse clicked position in order to display the fx at that point
				Vector3 mousePos = Camera.main.ScreenToWorldPoint (Input.mousePosition);

				float range = 1f;

				mousePos.z = 0;
				Debug.Log("Display Power");
				switch (TypeOfPower) {
				case "Fire":
					Debug.Log ("Fire power displayed");
					// Instantiate the prefab effect during runtime at the mouse position
					GameObject go = (GameObject) GameObject.Instantiate (FireParticle, mousePos, Quaternion.identity);
					Destroy (go, 2);
					range = this.FireRange;
					break;
				case "Lightning":
					Debug.Log ("Lightning power displayed");
					// Instantiate the prefab effect during runtime at the mouse position
					mousePos.y += 7.3f;
					GameObject goLightning = (GameObject) GameObject.Instantiate (LightningParticle, mousePos, Quaternion.identity);
					Destroy (goLightning, 2);
					range = this.LightningRange;
					break;
				case "Boost":
					GameObject goBoost = (GameObject) GameObject.Instantiate (BoostParticle, mousePos, Quaternion.identity);
					Destroy (goBoost, 2);
					break;
				default:
					break;
				}
				PowerIsActivated = false;
				this.timerPowerUsed = 0.1f;

				// Call the ritual manager to let him know a power was used
				if(RitualManager.Instance != null){
					Debug.Log("GameController.Update - Power activated - range : "+range+", mousePos : "+mousePos+", typePower : "+this.TypeOfPower);
					RitualManager.Instance.CreateRitual(new Vector2(mousePos.x, mousePos.y) , range, this.TypeOfPower, -1);
				}

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
