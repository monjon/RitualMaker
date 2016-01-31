using UnityEngine;
using System.Collections;
using System.Collections.Generic;

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
	public GameObject HealParticle;

   // [HideInInspector]
    public int ActionPoints = 3;
    [HideInInspector]
    public int TotalActionPointsUsed = 0;
    [HideInInspector]
    public int MaxActionPoints = 10;

	[Header("GodPowers")]
	public List<GodPower> GodPowersPrefab = new List<GodPower>();

	private List<GodPower> godPowers = new List<GodPower>();

	// Methods
	//

	public void Start(){

		foreach(GodPower gp in this.GodPowersPrefab){

			if(gp != null){

				GodPower instance = GameObject.Instantiate(gp);
				instance.transform.position = Vector3.zero;
				instance.transform.SetParent(this.transform);
				this.godPowers.Add(instance);

			}

		}

	}

	// Update is called once per frame
	void Update () {
		// Cooldown for power activated (TMP used for UI)
		if(!this.PowerIsActivated && this.ActionPoints > 0)
        {
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
					range = this.GetPowerByID("Fire").Range;
					break;
				case "Lightning":
					Debug.Log ("Lightning power displayed");
					// Instantiate the prefab effect during runtime at the mouse position
					mousePos.y += 7.3f;
					GameObject goLightning = (GameObject) GameObject.Instantiate (LightningParticle, mousePos, Quaternion.identity);
					Destroy (goLightning, 2);
					range = this.GetPowerByID("Lightning").Range;
					break;
				case "Boost":
					GameObject goBoost = (GameObject) GameObject.Instantiate (BoostParticle, mousePos, Quaternion.identity);
					Destroy (goBoost, 2);
					range = this.GetPowerByID("Boost").Range;
					break;
				case "Heal":
					GameObject goHeal = (GameObject)GameObject.Instantiate (HealParticle);
					goHeal.transform.position = mousePos;
					Destroy (goHeal, 2);
					break;
				default:
					break;
				}
				PowerIsActivated = false;
				this.timerPowerUsed = 0.1f;

				// Call the ritual manager to let him know a power was used
				if(RitualManager.Instance != null){
					Debug.Log("GameController.Update - Power activated - range : "+range+", mousePos : "+mousePos+", typePower : "+this.TypeOfPower);
					RitualManager.Instance.CreateRitual(new Vector2(mousePos.x, mousePos.y) , range, this.TypeOfPower, this.GetPowerByID(this.TypeOfPower).FearLove);

					// Put the power on cd
					this.GetPowerByID(this.TypeOfPower).UsePower();

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

	public GodPower GetPowerByID(string powerID){

		foreach(GodPower gp in this.godPowers){
			if(gp.PowerID == powerID){
				return gp;
			}
		}

		Debug.Log("GameController.GetPowerByID - ID not found : "+powerID);
		return null;

	}


}
